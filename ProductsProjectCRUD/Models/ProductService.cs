//Data Access Layer(DAL) in my .NET Application
// This layer will be responsible for connecting to your SQL Server database and executing the stored procedures....
//You can use ADO.NET directly or an ORM like Dapper for a more streamlined approach. Dapper is highly recommended as it's lightweight and performant.
// create classes that use System.Data.SqlClient (or Microsoft.Data.SqlClient for modern .NET) to call these stored procedures.
// Using ADO.NET (Manual Approach)

using Microsoft.Data.SqlClient;
using System.Data;
using ProductsProjectCRUD.Models;// Your Product, Category, Supplier models
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService
{
    private readonly string _connectionString;



    public ProductService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionStringProducts");
    }



    // ----------------------------------------------------------------------- //



    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.GetAllProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.GetInt32(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.GetInt32(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName"))
                            // Map other properties
                        });
                    }
                }
            }
        }
        return products;
    }





    // NEW: Get Categories associated with a specific Supplier's Products
    // THIS IS ONE OF THE METHODS YOU ARE MISSING
    public async Task<List<Category>> GetCategoriesBySupplierIdAsync(int supplierId)
    {
        var categories = new List<Category>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.sp_GetCategoriesBySupplierid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierID", supplierId);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"))
                        });
                    }
                }
            }
        }
        return categories;
    }






    // ----------------------------------------------------------------------- //
    // ----------------------------------------------------------------------- //
    // NEW: Get Products by Supplier ID and Category ID
    // THIS IS THE OTHER METHOD YOU ARE MISSING
    public async Task<List<Product>> GetProductsBySupplierIdAndCategoryIdAsync(int supplierId, int categoryId)
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.sp_GetProductsBySupplieridAndCategoryid", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierID", supplierId);
                command.Parameters.AddWithValue("@CategoryID", categoryId);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.GetInt32(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.GetInt32(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName"))
                        });
                    }
                }
            }
        }
        return products;
    }

    // ----------------------------------------------------------------------- //
    // ----------------------------------------------------------------------- //






    public async Task<Product> GetProductByIdAsync(int productId)
    {
        Product product = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.GetProductById", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", productId);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        product = new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.GetInt32(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.GetInt32(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified")),
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName"))
                        };
                    }
                }
            }
        }
        return product;
    }



    // ----------------------------------------------------------------------- //
    // ----------------------------------------------------------------------- //



    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.GetProductsByCategory", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", categoryId);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.GetInt32(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.GetInt32(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName"))
                        });
                    }
                }
            }
        }
        return products;
    }






    // ----------------------------------------------------------------------- //
    // ----------------------------------------------------------------------- //
    // ----------------------------------------------------------------------- //

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = new List<Category>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.GetAllCategories", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName"))
                        });
                    }
                }
            }
        }
        return categories;
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        var suppliers = new List<Supplier>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.GetAllSuppliers", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        suppliers.Add(new Supplier
                        {
                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            CompanyName = reader.GetString(reader.GetOrdinal("CompanyName"))
                        });
                    }
                }
            }
        }
        return suppliers;
    }

    // ----------------------------------------------------------------------- //
    // --- CRUD Methods Using CRUDFor_ManageProducts ---
    // ----------------------------------------------------------------------- //

    public async Task<int> CreateProductAsync(Product product)
    {
        int newProductId = 0;
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.CRUDFor_ManageProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActionType", "Create");
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
                command.Parameters.AddWithValue("@UnitsOnOrder", product.UnitsOnOrder);
                command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
                command.Parameters.AddWithValue("@Discontinued", product.Discontinued);

                await connection.OpenAsync();
                // SCOPE_IDENTITY() returns the new ID, which ExecuteScalarAsync retrieves.
                // We use Convert.ToInt32 because ExecuteScalarAsync returns object.
                newProductId = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }
        return newProductId;
    }

    public async Task UpdateProductAsync(Product product)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.CRUDFor_ManageProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActionType", "Update");
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@SupplierID", product.SupplierID);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                command.Parameters.AddWithValue("@QuantityPerUnit", product.QuantityPerUnit);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@UnitsInStock", product.UnitsInStock);
                command.Parameters.AddWithValue("@UnitsOnOrder", product.UnitsOnOrder);
                command.Parameters.AddWithValue("@ReorderLevel", product.ReorderLevel);
                command.Parameters.AddWithValue("@Discontinued", product.Discontinued);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                // The stored procedure for update returns a status message, but ExecuteNonQueryAsync
                // is sufficient if you just need to confirm completion.
                // If you need the message, you'd use ExecuteReaderAsync and read it.
            }
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.CRUDFor_ManageProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActionType", "Delete");
                command.Parameters.AddWithValue("@ProductID", productId);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                // Similar to update, the stored procedure for delete returns a status message.
            }
        }
    }

    // New method for searching products using the 'Read' action in CRUDFor_ManageProducts
    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {
        var products = new List<Product>();
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.CRUDFor_ManageProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ActionType", "Read");
                command.Parameters.AddWithValue("@SearchTerm", searchTerm ?? (object)DBNull.Value); // Handle null search term

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        products.Add(new Product
                        {
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                            CategoryName = reader.GetString(reader.GetOrdinal("CategoryName")),
                            QuantityPerUnit = reader.GetString(reader.GetOrdinal("QuantityPerUnit")),
                            UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                            UnitsInStock = reader.GetInt32(reader.GetOrdinal("UnitsInStock")),
                            UnitsOnOrder = reader.GetInt32(reader.GetOrdinal("UnitsOnOrder")),
                            ReorderLevel = reader.GetInt32(reader.GetOrdinal("ReorderLevel")),
                            Discontinued = reader.GetBoolean(reader.GetOrdinal("Discontinued")),
                            DateAdded = reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                            LastModified = reader.GetDateTime(reader.GetOrdinal("LastModified"))
                        });
                    }
                }
            }
        }
        return products;
    }
}