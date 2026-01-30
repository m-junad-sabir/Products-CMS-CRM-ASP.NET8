
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsProjectCRUD.BusinessLogicService;
using ProductsProjectCRUD.Models;


namespace ProductsProjectCRUD.Pages.Products
{
    public class CreateModel : PageModel
    {
        //private readonly ProductDataAccess _productDataAccess;
        //private readonly ILogger<CreateModel> _logger;

        //// Constructor for dependency injection
        //public CreateModel(ProductDataAccess productDataAccess, ILogger<CreateModel> logger)
        //{
        //    _productDataAccess = productDataAccess;
        //    _logger = logger;
        //}

        //// [BindProperty] makes the Product instance available for both GET (for initial form display)
        //// and POST (for receiving form data).
        //[BindProperty]
        //public Product Product { get; set; } = new Product(); // Initialize to avoid null reference

        //// SelectLists for dropdowns
        //public SelectList Categories { get; set; }
        //public SelectList Suppliers { get; set; }

        //// Method to populate dropdowns when the page is loaded (GET request)
        //public async Task<IActionResult> OnGetAsync()
        //{
        //    await PopulateDropdowns();
        //    return Page();
        //}

        //// Method to handle form submission (POST request)
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    // First, ensure the dropdowns are populated even if model state is invalid,
        //    // so the user doesn't lose their selections/options on a validation error.
        //    await PopulateDropdowns();

        //    // Validate the incoming model based on annotations in the Product model (e.g., [Required])
        //    if (!ModelState.IsValid)
        //    {
        //        // If validation fails, return the current page with validation errors displayed.
        //        return Page();
        //    }

        //    try
        //    {
        //        // Call the service layer to create the product.
        //        // The ProductID will be returned by the stored procedure after insertion.
        //        int newProductId = await _productDataAccess.CreateProductAsync(Product);

        //        // Log successful creation
        //        _logger.LogInformation($"Product '{Product.ProductName}' created successfully with ID: {newProductId}");

        //        // Add a success message (optional, for display on the next page)
        //        TempData["SuccessMessage"] = $"Product '{Product.ProductName}' added successfully!";

        //        // Redirect to the Index page after successful creation.
        //        return RedirectToPage("./Index");
        //    }
        //    catch (InvalidOperationException ex) // Catch specific business logic errors from ProductDataAccess
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message); // Add error to ModelState
        //        _logger.LogWarning(ex, "Business logic error while creating product.");
        //        return Page(); // Return page with error
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log any other unexpected errors
        //        _logger.LogError(ex, "Error creating product.");
        //        ModelState.AddModelError(string.Empty, "An unexpected error occurred while saving the product. Please try again.");
        //        return Page(); // Return the page with the error message
        //    }
        //}

        //// Helper method to populate SelectLists for categories and suppliers
        //private async Task PopulateDropdowns()
        //{
        //    var categories = await _productDataAccess.GetProductCategoriesAsync();
        //    Categories = new SelectList(categories, "CategoryID", "CategoryName");

        //    var suppliers = await _productDataAccess.GetProductSuppliersAsync();
        //    Suppliers = new SelectList(suppliers, "SupplierID", "CompanyName");
        //}

    }
}
