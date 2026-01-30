// Service Layer (Optional but Recommended)
// A service layer acts as an intermediary between your Razor Pages and the DAL. This helps to encapsulate business logic,
// making your code more modular and testable.


using ProductsProjectCRUD.Models; // Ensure this matches your models' namespace
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ProductsProjectCRUD.BusinessLogicService
{
    public class ProductDataAccess
    {
        private readonly ProductService _productService;

        // Constructor: ProductService is injected here
        public ProductDataAccess(ProductService productService)
        {
            _productService = productService;
        }

        // --- Business Logic / Intermediary Methods ---

        // NEW: Get Categories by Supplier ID (Ensure this one is also there if not)
        public async Task<List<Category>> GetCategoriesForSupplierAsync(int supplierId)
        {
            if (supplierId <= 0)
            {
                return new List<Category>();
            }
            return await _productService.GetCategoriesBySupplierIdAsync(supplierId);
        }


        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //
        // NEW: Get Products by Supplier ID and Category ID (THIS IS THE MISSING ONE)
        public async Task<List<Product>> GetProductsBySupplierAndCategoryAsync(int supplierId, int categoryId)
        {
            if (supplierId <= 0 || categoryId <= 0)
            {
                return new List<Product>();
            }
            return await _productService.GetProductsBySupplierIdAndCategoryIdAsync(supplierId, categoryId);
        }


        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //



        public async Task<List<Product>> GetProductsAsync()
        {
            // Here you can add business logic before or after calling the DAL.
            // For example:
            // - Filtering products based on user roles
            // - Applying caching logic
            // - Transforming data if needed

            return await _productService.GetAllProductsAsync();
        }



        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //

        public async Task<Product> GetProductDetailsAsync(int productId)
        {
            // Example: Add a validation or logging layer
            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive value.", nameof(productId));
            }
            return await _productService.GetProductByIdAsync(productId);
        }

        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //







        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //

        public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Category ID must be a positive value.", nameof(categoryId));
            }
            return await _productService.GetProductsByCategoryAsync(categoryId);
        }

        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            // Example: Clean or normalize search term
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _productService.GetAllProductsAsync(); // Return all if search term is empty
            }
            return await _productService.SearchProductsAsync(searchTerm.Trim());
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            // Example: Business rule - UnitPrice must be positive
            if (product.UnitPrice <= 0)
            {
                throw new InvalidOperationException("Product unit price must be greater than zero.");
            }
            // Example: Default values or sanity checks
            product.UnitsInStock = Math.Max(0, product.UnitsInStock);

            return await _productService.CreateProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            // Example: Ensure product exists before updating (optional, can also be handled by DAL)
            var existingProduct = await _productService.GetProductByIdAsync(product.ProductID);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.ProductID} not found for update.");
            }

            // More business rules or data manipulation can go here
            await _productService.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int productId)
        {
            // Example: Prevent deletion if product is currently on order
            // (Requires additional logic to check UnitsOnOrder)
            // Or simple existence check:
            var existingProduct = await _productService.GetProductByIdAsync(productId);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {productId} not found for deletion.");
            }

            await _productService.DeleteProductAsync(productId);
        }

        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //
        // ----------------------------------------------------------------------- //






        public async Task<List<Category>> GetProductCategoriesAsync()
        {
            return await _productService.GetAllCategoriesAsync();
        }

        public async Task<List<Supplier>> GetProductSuppliersAsync()
        {
            return await _productService.GetAllSuppliersAsync();

        }
    }
}
