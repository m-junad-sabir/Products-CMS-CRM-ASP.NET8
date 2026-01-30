
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Needed for SelectList
using Microsoft.Extensions.Logging;
using ProductsProjectCRUD.Models; // Your models
using ProductsProjectCRUD.BusinessLogicService; // Your ProductDataAccess service


namespace ProductsProjectCRUD.Pages.ProductsByCategory
{

    // ----------------------------------------------------------------------- //
    // ----------------------------------------------------------------------- //
    public class IndexModel : PageModel
    {
        private readonly ProductDataAccess _productDataAccess;
        private readonly ILogger<IndexModel> _logger;



        // Constructor for dependency injection
        public IndexModel(ProductDataAccess productDataAccess, ILogger<IndexModel> logger)
        {
            _productDataAccess = productDataAccess;
            _logger = logger;
        }



        // Properties to bind selected dropdown values
        [BindProperty(SupportsGet = true)] // Allows binding from query string on initial load
        public int? SelectedSupplierId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedProductId { get; set; } // For the specific product selected in the 3rd dropdown

        // SelectLists for dropdowns
        public SelectList Suppliers { get; set; }
        public SelectList Categories { get; set; }
        public SelectList ProductsDropdown { get; set; } // Naming to avoid conflict with Products list for table

        // This property will hold the list of products to display in the table
        public IList<Product> Products { get; set; } = new List<Product>();




        // ----------------------------------------------------------------------- //
        // OnGetAsync is called when the page is first loaded or when a GET request comes (e.g., after redirect)
        // ----------------------------------------------------------------------- //
        public async Task OnGetAsync()
        {
            try
            {
                // Always load suppliers initially
                await PopulateSuppliers();

                // If a supplier is selected, populate categories
                if (SelectedSupplierId.HasValue && SelectedSupplierId.Value > 0)
                {
                    await PopulateCategories(SelectedSupplierId.Value);
                }
                else
                {
                    // Initialize empty categories if no supplier selected
                    Categories = new SelectList(new List<Category>(), "CategoryID", "CategoryName");
                }

                // If supplier and category are selected, populate products dropdown
                if (SelectedSupplierId.HasValue && SelectedSupplierId.Value > 0 &&
                    SelectedCategoryId.HasValue && SelectedCategoryId.Value > 0)
                {
                    await PopulateProductsDropdown(SelectedSupplierId.Value, SelectedCategoryId.Value);
                }
                else
                {
                    // Initialize empty products dropdown
                    ProductsDropdown = new SelectList(new List<Product>(), "ProductID", "ProductName");
                }

                // If a specific product is selected, load only that product into the table
                if (SelectedProductId.HasValue && SelectedProductId.Value > 0)
                {
                    var product = await _productDataAccess.GetProductDetailsAsync(SelectedProductId.Value);
                    if (product != null)
                    {
                        Products.Add(product); // Add the single product to the list for the table
                    }
                }
                // If supplier and category are selected but no specific product, load all products for that combination
                else if (SelectedSupplierId.HasValue && SelectedSupplierId.Value > 0 &&
                         SelectedCategoryId.HasValue && SelectedCategoryId.Value > 0)
                {
                    Products = await _productDataAccess.GetProductsBySupplierAndCategoryAsync(SelectedSupplierId.Value, SelectedCategoryId.Value);
                }
                // Otherwise, the table remains empty (as per new requirement)
                else
                {
                    Products = new List<Product>(); // Ensure table is empty if no selection
                }
            }



            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading initial data for Index page.");
                TempData["ErrorMessage"] = "An error occurred while loading. Please try again later.";
                Products = new List<Product>(); // Ensure table is empty on error
                Suppliers = new SelectList(new List<Supplier>(), "SupplierID", "CompanyName");
                Categories = new SelectList(new List<Category>(), "CategoryID", "CategoryName");
                ProductsDropdown = new SelectList(new List<Product>(), "ProductID", "ProductName");
            }



        }



        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //

        // AJAX handler: Populates categories based on selected supplier
        public async Task<JsonResult> OnGetCategoriesForSupplier(int supplierId)
        {

            var categories = await _productDataAccess.GetCategoriesForSupplierAsync(supplierId);
            // Return as anonymous objects with Id and Text properties for easier JavaScript handling
            return new JsonResult(categories.Select(c => new { id = c.CategoryID, text = c.CategoryName }));
        }





        // AJAX handler: Populates products based on selected supplier and category
        public async Task<JsonResult> OnGetProductsForCategoryAndSupplier(int supplierId, int categoryId)
        {
            var products = await _productDataAccess.GetProductsBySupplierAndCategoryAsync(supplierId, categoryId);
            return new JsonResult(products);
        }


        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //


        // AJAX Handler: Fetches details for a single product to display in the table
        public async Task<JsonResult> OnGetProductDetails(int productId)
        {
            if (productId <= 0)
            {
                // You might return a specific error code or an empty object/list
                // For simplicity, returning null or an empty product object if ID is invalid
                _logger.LogWarning($"Attempted to fetch product details with invalid ID: {productId}");
                return new JsonResult(null);
            }

            try
            {
                var product = await _productDataAccess.GetProductDetailsAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {productId} not found.");
                    return new JsonResult(null); // Return null if product not found
                }
                return new JsonResult(product); // Return the full product object
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching details for product ID: {productId}");
                // Return an error status or specific JSON indicating failure
                Response.StatusCode = 500; // Set HTTP status code to indicate server error
                return new JsonResult(new { error = "Internal Server Error while fetching product details." });
            }
        }

        // ----------------------------------------------------------------------- //
        // Private helper methods to populate SelectLists
        private async Task PopulateSuppliers()
        {
            var suppliersList = await _productDataAccess.GetProductSuppliersAsync();
            Suppliers = new SelectList(suppliersList, "SupplierID", "CompanyName");
        }

        private async Task PopulateCategories(int supplierId)
        {
            var categoriesList = await _productDataAccess.GetCategoriesForSupplierAsync(supplierId);
            Categories = new SelectList(categoriesList, "CategoryID", "CategoryName");
        }

        private async Task PopulateProductsDropdown(int supplierId, int categoryId)
        {
            var productsList = await _productDataAccess.GetProductsBySupplierAndCategoryAsync(supplierId, categoryId);
            ProductsDropdown = new SelectList(productsList, "ProductID", "ProductName");
        }
    }
}
