using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductsProjectCRUD.BusinessLogicService;
using ProductsProjectCRUD.Models;

namespace ProductsProjectCRUD.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductDataAccess _productDataAccess;
        private readonly ILogger<IndexModel> _logger; // Optional: for logging

        // Constructor for dependency injection
        public IndexModel(ProductDataAccess productDataAccess, ILogger<IndexModel> logger)
        {
            _productDataAccess = productDataAccess;
            _logger = logger; // Inject logger (optional)
        }

        // This property will hold the list of products to display on the Razor Page
        public IList<Product> Products { get; set; } = new List<Product>();

        // This method is called when the HTTP GET request is made to the page
        public async Task OnGetAsync()
        {
            try
            {
                // Call the service layer to get all products
                Products = await _productDataAccess.GetProductsAsync();
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError(ex, "Error fetching products for Index page.");
                // Optionally, add an error message to display on the page
                TempData["ErrorMessage"] = "An error occurred while loading products. Please try again later.";
                // You might also want to set Products to an empty list to avoid null reference exceptions in the view
                Products = new List<Product>();
            }
        }

        // You could add other OnPost methods here if you had forms on the index page
        // For example, for a search function that uses OnPost:
        [BindProperty(SupportsGet = true)] // Allows SearchTerm to be bound from query string or form
        public string SearchTerm { get; set; }

        public async Task OnPostSearchAsync()
        {
            // This method would be called if you had a search form that POSTs to the same page
            // For example, if you have a form with asp-page-handler="Search"
            try
            {
                Products = await _productDataAccess.SearchProductsAsync(SearchTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products.");
                TempData["ErrorMessage"] = "An error occurred during search. Please try again later.";
                Products = new List<Product>();
            }
        }


    }
}
