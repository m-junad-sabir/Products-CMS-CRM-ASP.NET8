# Products Management CMS/CRM Application

A professional-grade, enterprise-ready **Content Management System (CMS)** and **Customer Relationship Management (CRM)** application built with **ASP.NET Core 8.0 Razor Pages**, designed for managing product catalogs, suppliers, and categories with comprehensive CRUD operations.

---

## ?? Table of Contents

- [Overview](#overview)
- [Technology Stack](#technology-stack)
- [System Architecture](#system-architecture)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Installation & Setup](#installation--setup)
- [Running the Application](#running-the-application)
- [Configuration](#configuration)
- [Architecture Layers](#architecture-layers)
- [Database Schema](#database-schema)
- [API Endpoints & Features](#api-endpoints--features)
- [Data Flow](#data-flow)
- [Development Guidelines](#development-guidelines)
- [Troubleshooting](#troubleshooting)
- [License](#license)

---

## ?? Overview

**Products Management CMS/CRM** is a robust web application that enables businesses to:

? Manage product catalogs with detailed specifications  
? Organize products by categories and suppliers  
? Track inventory levels and reorder information  
? Create, Read, Update, and Delete (CRUD) product records  
? Search and filter products efficiently  
? Maintain supplier and category relationships  

### Key Features

- **Async/Await Architecture** - Non-blocking, scalable database operations
- **Layered Architecture** - Clean separation of concerns (Presentation, Business Logic, Data Access)
- **Stored Procedure Based** - Pre-compiled SQL for optimal performance
- **Dependency Injection** - IoC container for loose coupling and testability
- **Error Handling** - Comprehensive exception handling and logging
- **Responsive UI** - Bootstrap 5 frontend with AJAX support
- **Type-Safe** - Full C# type safety with compile-time checking

---

## ?? Technology Stack

### Backend
| Component | Technology | Version |
|-----------|-----------|---------|
| **Framework** | ASP.NET Core | 8.0 |
| **UI Framework** | Razor Pages | 8.0 |
| **Data Access** | ADO.NET | Modern (Microsoft.Data.SqlClient) |
| **Database** | SQL Server | 2019+ |
| **ORM** | Stored Procedures | Custom mapping |
| **DI Container** | Built-in ASP.NET Core | 8.0.1 |
| **Configuration** | appsettings.json | - |
| **Logging** | ILogger | Built-in |
| **HTTP Client** | SqlClient | 6.0.2 |

### Frontend
| Component | Technology | Version |
|-----------|-----------|---------|
| **CSS Framework** | Bootstrap | 5.3.6 |
| **DOM Manipulation** | jQuery | Built-in |
| **HTTP Requests** | AJAX | Native |

### Development Tools
| Tool | Purpose |
|------|---------|
| Visual Studio 2022+ | IDE |
| SQL Server Management Studio (SSMS) | Database management |
| Git | Version control |
| NuGet | Package manager |

### NuGet Packages
```
- Microsoft.Data.SqlClient (6.0.2)
- Microsoft.EntityFrameworkCore (8.0.16) - Design packages
- Microsoft.AspNetCore.Authentication.Negotiate (8.0.16)
- Microsoft.Extensions.Configuration (8.0.0)
- Microsoft.Extensions.DependencyInjection (8.0.1)
- Bootstrap (5.3.6)
- Dapper (2.1.66) - Optional ORM
```

---

## ?? System Architecture

### High-Level Architecture Diagram

```
???????????????????????????????????????????????????????????????????
?                     CLIENT LAYER (Browser)                       ?
?              HTML/CSS/JavaScript Rendering                       ?
???????????????????????????????????????????????????????????????????
                         ? HTTP Requests/Responses
                         ?
???????????????????????????????????????????????????????????????????
?            PRESENTATION LAYER (Razor Pages)                      ?
?  ??????????????????????????????????????????????????????????????  ?
?  ? Pages/Products/Index.cshtml        [Product Listing]       ?  ?
?  ? Pages/Products/Create.cshtml       [Create New Product]    ?  ?
?  ? Pages/Products/Edit.cshtml         [Edit Product]          ?  ?
?  ? Pages/Products/Delete.cshtml       [Delete Product]        ?  ?
?  ? Pages/Products/Details.cshtml      [View Details]          ?  ?
?  ? Pages/ProductsByCategory/Index     [Category Filtering]    ?  ?
?  ??????????????????????????????????????????????????????????????  ?
?                         ? Dependency Injection
?                         ?
?  ??????????????????????????????????????????????????????????????  ?
?  ? Page Models (*.cshtml.cs)                                   ?  ?
?  ? - OnGetAsync()  : Handle GET requests                      ?  ?
?  ? - OnPostAsync() : Handle POST requests                     ?  ?
?  ? - OnGetXxx()    : Handle AJAX requests                     ?  ?
?  ??????????????????????????????????????????????????????????????  ?
??????????????????????????????????????????????????????????????????
                         ? Service Injection
                         ?
???????????????????????????????????????????????????????????????????
?        BUSINESS LOGIC LAYER (Service Layer)                      ?
?                                                                  ?
?  ProductDataAccess.cs                                           ?
?  ?? GetProductsAsync()                                         ?
?  ?? GetProductsByCategoryAsync()                               ?
?  ?? GetProductsBySupplierAndCategoryAsync()                    ?
?  ?? CreateProductAsync()          [Business Rule Validation]   ?
?  ?? UpdateProductAsync()          [Existence Checks]           ?
?  ?? DeleteProductAsync()          [Validation Logic]           ?
?  ?? SearchProductsAsync()                                      ?
?  ?? GetProductCategoriesAsync()                                ?
?  ?? GetProductSuppliersAsync()                                 ?
?                                                                  ?
?  Responsibilities:                                              ?
?  ? Business rule enforcement                                    ?
?  ? Data validation & sanitization                              ?
?  ? Logging & error handling                                    ?
?  ? Transaction management                                      ?
?  ? Cross-cutting concerns (caching, etc.)                      ?
??????????????????????????????????????????????????????????????????
                         ? Service Injection
                         ?
???????????????????????????????????????????????????????????????????
?          DATA ACCESS LAYER (Repository Layer)                    ?
?                                                                  ?
?  ProductService.cs                                              ?
?  ?? GetAllProductsAsync()         ? dbo.GetAllProducts        ?
?  ?? GetProductByIdAsync()         ? dbo.GetProductById        ?
?  ?? GetProductsByCategoryAsync()  ? dbo.GetProductsByCategory ?
?  ?? SearchProductsAsync()         ? dbo.CRUDFor_ManageProducts?
?  ?? CreateProductAsync()          ? dbo.CRUDFor_ManageProducts?
?  ?? UpdateProductAsync()          ? dbo.CRUDFor_ManageProducts?
?  ?? DeleteProductAsync()          ? dbo.CRUDFor_ManageProducts?
?  ?? GetAllCategoriesAsync()       ? dbo.GetAllCategories     ?
?  ?? GetAllSuppliersAsync()        ? dbo.GetAllSuppliers      ?
?  ?? GetCategoriesBySupplierIdAsync() ? dbo.sp_GetCategories... ?
?  ?? GetProductsBySupplierIdAndCategoryIdAsync() ? dbo.sp_Get...?
?                                                                  ?
?  Implementation Details:                                        ?
?  ? ADO.NET (Microsoft.Data.SqlClient)                          ?
?  ? Async SqlCommand execution                                  ?
?  ? SqlDataReader mapping to objects                            ?
?  ? Connection pooling & management                             ?
?  ? Parameter binding (SQL injection prevention)                ?
??????????????????????????????????????????????????????????????????
                         ? SqlConnection
                         ?
???????????????????????????????????????????????????????????????????
?              DATABASE LAYER (SQL Server)                         ?
?                                                                  ?
?  Stored Procedures:                                             ?
?  ?? dbo.GetAllProducts                                         ?
?  ?? dbo.GetProductById                                         ?
?  ?? dbo.GetProductsByCategory                                  ?
?  ?? dbo.GetAllCategories                                       ?
?  ?? dbo.GetAllSuppliers                                        ?
?  ?? dbo.sp_GetCategoriesBySupplierid                           ?
?  ?? dbo.sp_GetProductsBySupplieridAndCategoryid                ?
?  ?? dbo.CRUDFor_ManageProducts (Create/Read/Update/Delete)     ?
?                                                                  ?
?  Tables:                                                        ?
?  ?? Products (ProductID, ProductName, UnitPrice, etc.)        ?
?  ?? Categories (CategoryID, CategoryName)                     ?
?  ?? Suppliers (SupplierID, CompanyName, Contact Info)         ?
???????????????????????????????????????????????????????????????????
```

---

## ?? Project Structure

```
ProductsProjectCRUD/
?
??? ?? README.md                          [This file]
??? ?? ProductsProjectCRUD.csproj         [Project file]
??? ?? appsettings.json                   [Configuration]
??? ?? appsettings.Development.json       [Dev configuration]
??? ?? Program.cs                         [App startup & DI setup]
?
??? ?? Models/                            [Domain Models]
?   ??? Product.cs                        [Product entity]
?   ??? Category.cs                       [Category entity]
?   ??? Supplier.cs                       [Supplier entity]
?   ??? ProductService.cs                 [DATA ACCESS LAYER]
?   ??? ProductDataAccess.cs              [Empty - moved to BusinessLogicService]
?
??? ?? BusinessLogicService/              [BUSINESS LOGIC LAYER]
?   ??? ProductDataAccess.cs              [Service layer with business rules]
?
??? ?? Pages/                             [PRESENTATION LAYER]
?   ??? _Layout.cshtml                    [Master layout template]
?   ??? _ViewImports.cshtml               [Shared imports]
?   ??? _ViewStart.cshtml                 [View startup]
?   ??? Index.cshtml                      [Home page]
?   ??? Index.cshtml.cs                   [Home page model]
?   ??? Privacy.cshtml                    [Privacy page]
?   ??? Privacy.cshtml.cs                 [Privacy model]
?   ??? Error.cshtml                      [Error page]
?   ??? Error.cshtml.cs                   [Error model]
?   ?
?   ??? ?? Shared/                        [Shared components]
?   ?   ??? _Layout.cshtml                [Main layout]
?   ?   ??? _ValidationScriptsPartial.cshtml [Validation scripts]
?   ?
?   ??? ?? Products/                      [Product management pages]
?   ?   ??? Index.cshtml                  [Product list view]
?   ?   ??? Index.cshtml.cs               [Product list logic]
?   ?   ??? Create.cshtml                 [Create product view]
?   ?   ??? Create.cshtml.cs              [Create logic]
?   ?   ??? Edit.cshtml                   [Edit product view]
?   ?   ??? Edit.cshtml.cs                [Edit logic]
?   ?   ??? Delete.cshtml                 [Delete confirmation view]
?   ?   ??? Delete.cshtml.cs              [Delete logic]
?   ?   ??? Details.cshtml                [Product details view]
?   ?   ??? Details.cshtml.cs             [Details logic]
?   ?
?   ??? ?? ProductsByCategory/            [Category-based filtering]
?       ??? Index.cshtml                  [Category filter view]
?       ??? Index.cshtml.cs               [Multi-level filtering logic]
?
??? ?? wwwroot/                           [Static files]
?   ??? css/                              [Stylesheets]
?   ??? js/                               [JavaScript files]
?   ??? lib/                              [Third-party libraries]
?
??? ?? obj/                               [Build artifacts (auto-generated)]
```

---

## ?? Prerequisites

### System Requirements
- **OS**: Windows 10+, macOS, or Linux
- **RAM**: Minimum 8GB (recommended 16GB)
- **Disk Space**: 2GB for .NET SDK + project files

### Required Software
1. **Visual Studio 2022** (Community or higher)
   - Download: https://visualstudio.microsoft.com/
   - Required Workloads:
     - ASP.NET and web development
     - .NET desktop development

2. **.NET 8.0 SDK**
   - Download: https://dotnet.microsoft.com/download/dotnet/8.0
   - Verify: `dotnet --version`

3. **SQL Server 2019 or Later** (Developer Edition is free)
   - Download: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
   - Or use **Azure SQL Database** for cloud deployment

4. **SQL Server Management Studio (SSMS)**
   - Download: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms
   - For managing databases and executing scripts

5. **Git**
   - Download: https://git-scm.com/
   - Verify: `git --version`

---

## ?? Installation & Setup

### Step 1: Clone the Repository

```bash
# Clone the repository
git clone https://github.com/yourusername/ProductsProjectCRUD.git

# Navigate to project directory
cd ProductsProjectCRUD
```

### Step 2: Restore NuGet Dependencies

```bash
# Restore all NuGet packages
dotnet restore
```

**or** in Visual Studio:
- Right-click Solution ? "Restore NuGet Packages"

### Step 3: Configure Database Connection

#### Option A: Local SQL Server (Recommended for Development)

1. **Open `appsettings.json`**:
```json
{
  "ConnectionStrings": {
    "ConnectionStringProducts": "Server=YOUR_SERVER_NAME;Database=ProductsDB;Integrated Security=true;Encrypt=false;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

2. **Replace `YOUR_SERVER_NAME`** with:
   - Local instance: `(localdb)\\mssqllocaldb` or `.` or `localhost`
   - Named instance: `COMPUTER_NAME\\SQLEXPRESS`
   - Remote server: `server.example.com`

3. **Verify your SQL Server instance**:
```bash
# In SQL Server Management Studio
SELECT @@SERVERNAME;
```

#### Option B: Azure SQL Database (Cloud Deployment)

1. **Modify connection string**:
```json
{
  "ConnectionStrings": {
    "ConnectionStringProducts": "Server=tcp:yourserver.database.windows.net,1433;Initial Catalog=ProductsDB;Persist Security Info=False;User ID=yourusername;Password=YourPassword123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
```

### Step 4: Create and Seed the Database

#### Using SQL Scripts

1. **Open SQL Server Management Studio (SSMS)**
2. **Create database**:
```sql
CREATE DATABASE ProductsDB;
GO
```

3. **Execute table creation script** (SQL/CreateTables.sql):
```sql
USE ProductsDB;
GO

-- Create Suppliers table
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(40) NOT NULL,
    ContactName NVARCHAR(30),
    ContactTitle NVARCHAR(30),
    Address NVARCHAR(60),
    City NVARCHAR(15),
    Region NVARCHAR(15),
    PostalCode NVARCHAR(10),
    Country NVARCHAR(15),
    Phone NVARCHAR(24),
    Email NVARCHAR(100)
);

-- Create Categories table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(15) NOT NULL,
    Description NVARCHAR(MAX)
);

-- Create Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(40) NOT NULL,
    SupplierID INT NOT NULL,
    CategoryID INT NOT NULL,
    QuantityPerUnit NVARCHAR(20),
    UnitPrice DECIMAL(10, 2),
    UnitsInStock INT,
    UnitsOnOrder INT,
    ReorderLevel INT,
    Discontinued BIT DEFAULT 0,
    DateAdded DATETIME DEFAULT GETDATE(),
    LastModified DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);
GO
```

4. **Create stored procedures** (SQL/CreateStoredProcedures.sql):
```sql
USE ProductsDB;
GO

-- Get all products
CREATE PROCEDURE dbo.GetAllProducts
AS
BEGIN
    SELECT 
        p.ProductID,
        p.ProductName,
        p.SupplierID,
        s.CompanyName AS SupplierName,
        p.CategoryID,
        c.CategoryName,
        p.QuantityPerUnit,
        p.UnitPrice,
        p.UnitsInStock,
        p.UnitsOnOrder,
        p.ReorderLevel,
        p.Discontinued,
        p.DateAdded,
        p.LastModified
    FROM Products p
    INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
    ORDER BY p.ProductName;
END;
GO

-- Get product by ID
CREATE PROCEDURE dbo.GetProductById
    @ProductID INT
AS
BEGIN
    SELECT 
        p.ProductID,
        p.ProductName,
        p.SupplierID,
        s.CompanyName AS SupplierName,
        p.CategoryID,
        c.CategoryName,
        p.QuantityPerUnit,
        p.UnitPrice,
        p.UnitsInStock,
        p.UnitsOnOrder,
        p.ReorderLevel,
        p.Discontinued,
        p.DateAdded,
        p.LastModified
    FROM Products p
    INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
    WHERE p.ProductID = @ProductID;
END;
GO

-- Get products by category
CREATE PROCEDURE dbo.GetProductsByCategory
    @CategoryID INT
AS
BEGIN
    SELECT 
        p.ProductID,
        p.ProductName,
        p.SupplierID,
        s.CompanyName AS SupplierName,
        p.CategoryID,
        c.CategoryName,
        p.QuantityPerUnit,
        p.UnitPrice,
        p.UnitsInStock,
        p.UnitsOnOrder,
        p.ReorderLevel,
        p.Discontinued,
        p.DateAdded,
        p.LastModified
    FROM Products p
    INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
    WHERE p.CategoryID = @CategoryID
    ORDER BY p.ProductName;
END;
GO

-- Get all categories
CREATE PROCEDURE dbo.GetAllCategories
AS
BEGIN
    SELECT CategoryID, CategoryName
    FROM Categories
    ORDER BY CategoryName;
END;
GO

-- Get all suppliers
CREATE PROCEDURE dbo.GetAllSuppliers
AS
BEGIN
    SELECT SupplierID, CompanyName
    FROM Suppliers
    ORDER BY CompanyName;
END;
GO

-- Get categories by supplier ID
CREATE PROCEDURE dbo.sp_GetCategoriesBySupplierid
    @SupplierID INT
AS
BEGIN
    SELECT DISTINCT c.CategoryID, c.CategoryName
    FROM Categories c
    INNER JOIN Products p ON c.CategoryID = p.CategoryID
    WHERE p.SupplierID = @SupplierID
    ORDER BY c.CategoryName;
END;
GO

-- Get products by supplier and category
CREATE PROCEDURE dbo.sp_GetProductsBySupplieridAndCategoryid
    @SupplierID INT,
    @CategoryID INT
AS
BEGIN
    SELECT 
        p.ProductID,
        p.ProductName,
        p.SupplierID,
        s.CompanyName AS SupplierName,
        p.CategoryID,
        c.CategoryName,
        p.QuantityPerUnit,
        p.UnitPrice,
        p.UnitsInStock,
        p.UnitsOnOrder,
        p.ReorderLevel,
        p.Discontinued,
        p.DateAdded,
        p.LastModified
    FROM Products p
    INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID
    INNER JOIN Categories c ON p.CategoryID = c.CategoryID
    WHERE p.SupplierID = @SupplierID 
      AND p.CategoryID = @CategoryID
    ORDER BY p.ProductName;
END;
GO

-- CRUD operations
CREATE PROCEDURE dbo.CRUDFor_ManageProducts
    @ActionType NVARCHAR(10),
    @ProductID INT = NULL,
    @ProductName NVARCHAR(40) = NULL,
    @SupplierID INT = NULL,
    @CategoryID INT = NULL,
    @QuantityPerUnit NVARCHAR(20) = NULL,
    @UnitPrice DECIMAL(10, 2) = NULL,
    @UnitsInStock INT = NULL,
    @UnitsOnOrder INT = NULL,
    @ReorderLevel INT = NULL,
    @Discontinued BIT = NULL,
    @SearchTerm NVARCHAR(MAX) = NULL
AS
BEGIN
    IF @ActionType = 'Create'
    BEGIN
        INSERT INTO Products 
        (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, DateAdded, LastModified)
        VALUES 
        (@ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued, GETDATE(), GETDATE());
        
        SELECT SCOPE_IDENTITY() AS NewProductID;
    END
    ELSE IF @ActionType = 'Update'
    BEGIN
        UPDATE Products
        SET 
            ProductName = @ProductName,
            SupplierID = @SupplierID,
            CategoryID = @CategoryID,
            QuantityPerUnit = @QuantityPerUnit,
            UnitPrice = @UnitPrice,
            UnitsInStock = @UnitsInStock,
            UnitsOnOrder = @UnitsOnOrder,
            ReorderLevel = @ReorderLevel,
            Discontinued = @Discontinued,
            LastModified = GETDATE()
        WHERE ProductID = @ProductID;
        
        SELECT @@ROWCOUNT AS RowsAffected;
    END
    ELSE IF @ActionType = 'Delete'
    BEGIN
        DELETE FROM Products
        WHERE ProductID = @ProductID;
        
        SELECT @@ROWCOUNT AS RowsAffected;
    END
    ELSE IF @ActionType = 'Read'
    BEGIN
        SELECT 
            p.ProductID,
            p.ProductName,
            p.SupplierID,
            s.CompanyName AS SupplierName,
            p.CategoryID,
            c.CategoryName,
            p.QuantityPerUnit,
            p.UnitPrice,
            p.UnitsInStock,
            p.UnitsOnOrder,
            p.ReorderLevel,
            p.Discontinued,
            p.DateAdded,
            p.LastModified
        FROM Products p
        INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID
        INNER JOIN Categories c ON p.CategoryID = c.CategoryID
        WHERE p.ProductName LIKE '%' + @SearchTerm + '%'
           OR p.QuantityPerUnit LIKE '%' + @SearchTerm + '%'
           OR s.CompanyName LIKE '%' + @SearchTerm + '%'
           OR c.CategoryName LIKE '%' + @SearchTerm + '%'
        ORDER BY p.ProductName;
    END
END;
GO
```

5. **Insert sample data** (SQL/SampleData.sql):
```sql
USE ProductsDB;
GO

-- Insert Suppliers
INSERT INTO Suppliers (CompanyName, ContactName, ContactTitle, City, Country, Phone)
VALUES 
('Supplier A Inc', 'John Smith', 'Procurement Manager', 'New York', 'USA', '555-0001'),
('Supplier B Ltd', 'Jane Doe', 'Sales Manager', 'Toronto', 'Canada', '555-0002'),
('Supplier C Corp', 'Ahmed Khan', 'Export Manager', 'Dubai', 'UAE', '555-0003');

-- Insert Categories
INSERT INTO Categories (CategoryName, Description)
VALUES 
('Electronics', 'Electronic products and components'),
('Software', 'Software and digital products'),
('Hardware', 'Physical hardware components'),
('Services', 'Professional services');

-- Insert Products
INSERT INTO Products (ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued)
VALUES 
('Laptop Pro', 1, 1, 'Per Unit', 999.99, 50, 10, 20, 0),
('Office Suite', 2, 2, 'Per License', 299.99, 100, 0, 50, 0),
('Server Hardware', 1, 3, 'Per Unit', 2999.99, 5, 2, 2, 0),
('Consulting Service', 3, 4, 'Per Hour', 150.00, 0, 0, 0, 0);

GO
```

### Step 5: Update Project Configuration

1. **Open Visual Studio**
2. **Open the solution file**: `ProductsProjectCRUD.sln`
3. **In Package Manager Console** (Tools ? NuGet Package Manager ? Package Manager Console):
   ```powershell
   # Verify all packages are restored
   dotnet restore
   ```

4. **Build the solution**:
   ```bash
   Ctrl + Shift + B
   ```

---

## ?? Running the Application

### Option 1: Visual Studio

1. **Set startup project** to `ProductsProjectCRUD`
2. **Press** `F5` or click **Start Debugging**
3. **Application opens** at `https://localhost:5001`
4. **Navigate** to `/Products` to see the product list

### Option 2: .NET CLI

```bash
# Navigate to project directory
cd ProductsProjectCRUD

# Run the application
dotnet run

# Application will be available at:
# https://localhost:5001
# http://localhost:5000
```

### Option 3: Docker (Optional)

```bash
# Create Dockerfile
docker build -t products-cms-crm .

# Run container
docker run -p 5001:443 -p 5000:80 products-cms-crm

# Access at http://localhost:5000
```

---

## ?? Configuration

### appsettings.json

```json
{
  "ConnectionStrings": {
    "ConnectionStringProducts": "Server=(localdb)\\mssqllocaldb;Database=ProductsDB;Integrated Security=true;Encrypt=false;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "ProductsProjectCRUD": "Debug"
    }
  },
  "AllowedHosts": "*"
}
```

### appsettings.Development.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Information",
      "Microsoft.AspNetCore": "Information"
    }
  },
  "DetailedErrors": true
}
```

### Program.cs Configuration

Key DI registrations:

```csharp
// Dependency Injection Setup
builder.Services.AddRazorPages()
    .AddJsonOptions(options => 
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = 
            JsonNamingPolicy.CamelCase;
    });

// Register Data Access Layer
builder.Services.AddScoped<ProductService>(sp =>
    new ProductService(sp.GetRequiredService<IConfiguration>())
);

// Register Business Logic Layer
builder.Services.AddScoped<ProductDataAccess>();
```

---

## ??? Architecture Layers

### Layer Overview

Your application follows a **3-tier layered architecture** pattern with clear separation of concerns:

#### **Layer 1: Presentation Layer (UI)**
**Location**: `Pages/` folder  
**Technology**: Razor Pages (.cshtml + .cshtml.cs)  
**Responsibility**: User interface and request handling

```csharp
// Example: Pages/Products/Index.cshtml.cs
public class IndexModel : PageModel
{
    private readonly ProductDataAccess _productDataAccess;
    
    public IList<Product> Products { get; set; }
    
    public async Task OnGetAsync()
    {
        // Handler for HTTP GET request
        Products = await _productDataAccess.GetProductsAsync();
    }
    
    public async Task OnPostSearchAsync()
    {
        // Handler for HTTP POST request (search)
        Products = await _productDataAccess.SearchProductsAsync(SearchTerm);
    }
}
```

**Features**:
- Request handling with async handlers (OnGetAsync, OnPostAsync, etc.)
- Dependency injection of service layer
- Page model binding
- Error handling and logging
- Session/TempData management

---

#### **Layer 2: Business Logic / Service Layer**
**Location**: `BusinessLogicService/ProductDataAccess.cs`  
**Technology**: Plain C# classes  
**Responsibility**: Business rules, validation, and orchestration

```csharp
public class ProductDataAccess
{
    private readonly ProductService _productService;
    
    public async Task<List<Product>> GetProductsAsync()
    {
        // Simply delegate to DAL
        return await _productService.GetAllProductsAsync();
    }
    
    public async Task<int> CreateProductAsync(Product product)
    {
        // Business Rule: UnitPrice must be positive
        if (product.UnitPrice <= 0)
            throw new InvalidOperationException("Price must be > 0");
        
        // Business Rule: Default UnitsInStock to 0 if negative
        product.UnitsInStock = Math.Max(0, product.UnitsInStock);
        
        // Call DAL
        return await _productService.CreateProductAsync(product);
    }
    
    public async Task UpdateProductAsync(Product product)
    {
        // Business Rule: Product must exist before updating
        var existing = await _productService.GetProductByIdAsync(product.ProductID);
        if (existing == null)
            throw new KeyNotFoundException($"Product {product.ProductID} not found");
        
        // Call DAL
        await _productService.UpdateProductAsync(product);
    }
    
    public async Task DeleteProductAsync(int productId)
    {
        // Business Rule: Product must exist before deleting
        var existing = await _productService.GetProductByIdAsync(productId);
        if (existing == null)
            throw new KeyNotFoundException($"Product {productId} not found");
        
        // Call DAL
        await _productService.DeleteProductAsync(productId);
    }
    
    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {
        // Business Rule: Trim and validate search term
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await _productService.GetAllProductsAsync();
        
        return await _productService.SearchProductsAsync(searchTerm.Trim());
    }
}
```

**Key Responsibilities**:
- ? Input validation and sanitization
- ? Business rule enforcement
- ? Cross-cutting concerns (logging, caching)
- ? Transaction coordination
- ? Data transformation
- ? Exception handling

---

#### **Layer 3: Data Access Layer (Repository Pattern)**
**Location**: `Models/ProductService.cs`  
**Technology**: ADO.NET with SqlConnection and SqlCommand  
**Responsibility**: Database operations and stored procedure execution

```csharp
public class ProductService
{
    private readonly string _connectionString;
    
    public ProductService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ConnectionStringProducts");
    }
    
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
                            // ... map other properties
                        });
                    }
                }
            }
        }
        
        return products;
    }
    
    public async Task<int> CreateProductAsync(Product product)
    {
        int newProductId = 0;
        
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("dbo.CRUDFor_ManageProducts", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                // Add parameters (prevents SQL injection)
                command.Parameters.AddWithValue("@ActionType", "Create");
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                // ... add other parameters
                
                await connection.OpenAsync();
                newProductId = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }
        
        return newProductId;
    }
}
```

**Key Responsibilities**:
- ? SQL execution (queries, stored procedures)
- ? Connection management
- ? Parameter binding (SQL injection prevention)
- ? Result mapping to objects
- ? Connection pooling
- ? Async database operations

---

### Data Flow Diagram

```
User Request
    ?
[Razor Page Handler] (OnGetAsync, OnPostAsync)
    ?
[Dependency Injection] - ProductDataAccess injected
    ?
[Business Logic Layer] - ProductDataAccess
    ?? Validate input
    ?? Apply business rules
    ?? Log operation
    ?? Call DAL method
    ?
[Data Access Layer] - ProductService
    ?? Create SqlConnection
    ?? Create SqlCommand with StoredProcedure
    ?? Add parameters
    ?? Open connection (async)
    ?? Execute query/procedure (async)
    ?? Map results to objects
    ?? Close connection
    ?
[SQL Server Database]
    ?? Execute stored procedure
    ?? Fetch/modify data
    ?? Return results
    ?
[Data Access Layer] Maps to objects
    ?
[Business Logic Layer] Post-processing
    ?
[Razor Page Model] Binds to properties
    ?
[Razor Page View] Renders HTML
    ?
HTTP Response to Browser
```

---

### Dependency Injection Flow

```
Program.cs
    ?
builder.Services.AddScoped<ProductService>(...)
builder.Services.AddScoped<ProductDataAccess>()
    ?
Dependency Container
    ?? ProductService
    ?   ?? Requires: IConfiguration
    ?
    ?? ProductDataAccess
        ?? Requires: ProductService
    ?
Razor Page Model Constructor
    public IndexModel(ProductDataAccess productDataAccess)
    {
        _productDataAccess = productDataAccess;
    }
    ?
Container automatically injects ProductDataAccess
    ?
PageModel uses _productDataAccess methods
```

---

## ?? Database Schema

### Entity-Relationship Diagram

```
???????????????????????
?    Suppliers        ?
???????????????????????
? PK: SupplierID      ?
? CompanyName         ?
? ContactName         ?
? ContactTitle        ?
? Address             ?
? City                ?
? Region              ?
? PostalCode          ?
? Country             ?
? Phone               ?
? Email               ?
???????????????????????
           ?
           ? 1:N
           ?
???????????????????????????????
?     Products                 ?
????????????????????????????????
? PK: ProductID                ?
? ProductName                  ?
? FK: SupplierID ??????????    ?
? FK: CategoryID ??????????    ?
? QuantityPerUnit        ??    ?
? UnitPrice              ??    ?
? UnitsInStock           ??    ?
? UnitsOnOrder           ??    ?
? ReorderLevel           ??    ?
? Discontinued           ??    ?
? DateAdded              ??    ?
? LastModified           ??    ?
????????????????????????????????
           ?               ?
           ? 1:N           ? 1:N
           ?               ?
           ?????????????????
                   ?
        ???????????????????????
        ?   Categories        ?
        ???????????????????????
        ? PK: CategoryID      ?
        ? CategoryName        ?
        ? Description         ?
        ???????????????????????
```

### Table Details

#### **Products Table**
```sql
ProductID (INT, PK, Identity)     -- Primary Key
ProductName (NVARCHAR(40))        -- Product name
SupplierID (INT, FK)              -- Foreign Key to Suppliers
CategoryID (INT, FK)              -- Foreign Key to Categories
QuantityPerUnit (NVARCHAR(20))    -- Unit packaging (e.g., "24 bottles")
UnitPrice (DECIMAL(10,2))         -- Price per unit
UnitsInStock (INT)                -- Current inventory
UnitsOnOrder (INT)                -- On-order quantity
ReorderLevel (INT)                -- Trigger point for reorder
Discontinued (BIT)                -- Product status (0=Active, 1=Discontinued)
DateAdded (DATETIME)              -- Creation timestamp
LastModified (DATETIME)           -- Last update timestamp
```

#### **Suppliers Table**
```sql
SupplierID (INT, PK, Identity)    -- Primary Key
CompanyName (NVARCHAR(40))        -- Supplier company name
ContactName (NVARCHAR(30))        -- Contact person
ContactTitle (NVARCHAR(30))       -- Contact title
Address (NVARCHAR(60))            -- Street address
City (NVARCHAR(15))               -- City
Region (NVARCHAR(15))             -- State/Province
PostalCode (NVARCHAR(10))         -- ZIP/Postal code
Country (NVARCHAR(15))            -- Country
Phone (NVARCHAR(24))              -- Contact phone
Email (NVARCHAR(100))             -- Contact email
```

#### **Categories Table**
```sql
CategoryID (INT, PK, Identity)    -- Primary Key
CategoryName (NVARCHAR(15))       -- Category name
Description (NVARCHAR(MAX))       -- Category description
```

---

## ?? API Endpoints & Features

### Product Management Routes

| Feature | Route | Method | Handler | Description |
|---------|-------|--------|---------|-------------|
| **List Products** | `/Products` | GET | OnGetAsync() | Display all products |
| **Create Product** | `/Products/Create` | GET | OnGetAsync() | Show create form |
| **Create Product** | `/Products/Create` | POST | OnPostAsync() | Save new product |
| **Edit Product** | `/Products/Edit/{id}` | GET | OnGetAsync() | Show edit form |
| **Edit Product** | `/Products/Edit/{id}` | POST | OnPostAsync() | Update product |
| **Delete Product** | `/Products/Delete/{id}` | GET | OnGetAsync() | Show delete confirmation |
| **Delete Product** | `/Products/Delete/{id}` | POST | OnPostAsync() | Remove product |
| **View Details** | `/Products/Details/{id}` | GET | OnGetAsync() | Show product details |
| **Search** | `/Products` | POST | OnPostSearchAsync() | Search products |

### Category Filtering Routes

| Feature | Route | Method | Handler | Description |
|---------|-------|--------|---------|-------------|
| **Filter by Category** | `/ProductsByCategory` | GET | OnGetAsync() | Multi-level filtering UI |
| **Get Categories** | `/ProductsByCategory?handler=CategoriesForSupplier` | GET | OnGetCategoriesForSupplier() | AJAX endpoint |
| **Get Products** | `/ProductsByCategory?handler=ProductsForCategoryAndSupplier` | GET | OnGetProductsForCategoryAndSupplier() | AJAX endpoint |
| **Get Details** | `/ProductsByCategory?handler=ProductDetails` | GET | OnGetProductDetails() | AJAX endpoint |

### Service Methods

#### ProductDataAccess (Business Logic Layer)

```csharp
// Read Operations
Task<List<Product>> GetProductsAsync()
Task<Product> GetProductDetailsAsync(int productId)
Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
Task<List<Product>> GetProductsBySupplierAndCategoryAsync(int supplierId, int categoryId)
Task<List<Product>> SearchProductsAsync(string searchTerm)
Task<List<Category>> GetCategoriesForSupplierAsync(int supplierId)
Task<List<Category>> GetProductCategoriesAsync()
Task<List<Supplier>> GetProductSuppliersAsync()

// Write Operations
Task<int> CreateProductAsync(Product product)
Task UpdateProductAsync(Product product)
Task DeleteProductAsync(int productId)
```

---

## ?? Data Flow

### Complete Example: Creating a Product

```
1. USER INTERACTION
   ?
   User fills form on /Products/Create and clicks "Save"
   
2. FORM SUBMISSION
   ?
   Form POSTs to /Products/Create with Product data
   
3. RAZOR PAGE MODEL (Presentation Layer)
   ?
   CreateModel.OnPostAsync() is invoked
   ?? Receives form data via model binding
   ?? Calls: _productDataAccess.CreateProductAsync(product)
   
4. BUSINESS LOGIC LAYER
   ?
   ProductDataAccess.CreateProductAsync(product)
   ?? Validates: if (product.UnitPrice <= 0) throw exception
   ?? Sets defaults: UnitsInStock = Math.Max(0, UnitsInStock)
   ?? Delegates to: _productService.CreateProductAsync(product)
   
5. DATA ACCESS LAYER
   ?
   ProductService.CreateProductAsync(product)
   ?? Creates SqlConnection from connection string
   ?? Creates SqlCommand("dbo.CRUDFor_ManageProducts")
   ?? Sets CommandType = CommandType.StoredProcedure
   ?? Adds parameters:
   ?  ?? @ActionType = "Create"
   ?  ?? @ProductName = product.ProductName
   ?  ?? @SupplierID = product.SupplierID
   ?  ?? @CategoryID = product.CategoryID
   ?  ?? ... other parameters
   ?? Opens connection asynchronously: await connection.OpenAsync()
   ?? Executes scalar: int id = await command.ExecuteScalarAsync()
   ?  (Returns SCOPE_IDENTITY() - new ProductID from stored procedure)
   ?? Returns: newProductId
   
6. STORED PROCEDURE EXECUTION (SQL Server)
   ?
   dbo.CRUDFor_ManageProducts with @ActionType = 'Create'
   ?? INSERT INTO Products (ProductName, SupplierID, ...)
   ?  VALUES (@ProductName, @SupplierID, ...)
   ?? SET @NewProductID = SCOPE_IDENTITY()
   ?? Returns: New ProductID
   
7. RESPONSE CHAIN
   ?
   ProductService returns: newProductId (int)
   ?
   ProductDataAccess returns: newProductId
   ?
   CreateModel stores: int newId = result
   ?? Sets: TempData["SuccessMessage"] = "Product created"
   ?? Redirects: RedirectToPage("./Index")
   
8. USER SEES
   ?
   Redirected to /Products with success message
   New product appears in list
```

---

## ?? Development Guidelines

### Project Structure Conventions

1. **Keep layers separate**
   - UI logic in Razor Pages (Pages/)
   - Business logic in services (BusinessLogicService/)
   - Data access in ProductService (Models/)

2. **Async/Await always**
   - Use async methods for all I/O operations
   - Use `await` instead of `.Result`
   - Prevents deadlocks and improves scalability

3. **Null safety**
   - Enable nullable reference types (done in .csproj)
   - Handle nulls explicitly
   - Use null-coalescing operator (`??`)

4. **Error handling**
   - Catch exceptions in Page Models
   - Log errors with ILogger
   - Display user-friendly messages via TempData

5. **Parameter binding**
   - Always use `@Parameter` binding in SQL
   - Never concatenate strings into SQL
   - Prevents SQL injection

### Adding New Features

#### Example: Add "Get Products by Price Range"

**Step 1: Create stored procedure** (SQL Server)
```sql
CREATE PROCEDURE dbo.sp_GetProductsByPriceRange
    @MinPrice DECIMAL(10, 2),
    @MaxPrice DECIMAL(10, 2)
AS
BEGIN
    SELECT ... FROM Products
    WHERE UnitPrice BETWEEN @MinPrice AND @MaxPrice
    ORDER BY UnitPrice;
END;
GO
```

**Step 2: Add method to ProductService** (Data Access Layer)
```csharp
public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
{
    var products = new List<Product>();
    using (var connection = new SqlConnection(_connectionString))
    {
        using (var command = new SqlCommand("dbo.sp_GetProductsByPriceRange", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@MinPrice", minPrice);
            command.Parameters.AddWithValue("@MaxPrice", maxPrice);
            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    products.Add(MapProductFromReader(reader));
                }
            }
        }
    }
    return products;
}
```

**Step 3: Add method to ProductDataAccess** (Business Logic Layer)
```csharp
public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
{
    // Validation
    if (minPrice < 0 || maxPrice < 0)
        throw new ArgumentException("Prices must be positive");
    if (minPrice > maxPrice)
        throw new ArgumentException("MinPrice cannot be greater than MaxPrice");
    
    // Delegate to DAL
    return await _productService.GetProductsByPriceRangeAsync(minPrice, maxPrice);
}
```

**Step 4: Create Razor Page** (Presentation Layer)
```csharp
// Pages/Products/ByPrice.cshtml.cs
public class ByPriceModel : PageModel
{
    private readonly ProductDataAccess _productDataAccess;
    
    [BindProperty(SupportsGet = true)]
    public decimal? MinPrice { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public decimal? MaxPrice { get; set; }
    
    public IList<Product> Products { get; set; }
    
    public async Task OnGetAsync()
    {
        if (MinPrice.HasValue && MaxPrice.HasValue)
        {
            Products = await _productDataAccess.GetProductsByPriceRangeAsync(
                MinPrice.Value, MaxPrice.Value);
        }
    }
}
```

**Step 5: Create Razor View**
```html
<!-- Pages/Products/ByPrice.cshtml -->
@page
@model ByPriceModel
@{
    ViewData["Title"] = "Products by Price Range";
}

<div class="container">
    <h1>Filter by Price</h1>
    
    <form method="get">
        <div class="row">
            <div class="col-md-3">
                <label>Min Price</label>
                <input type="number" asp-for="MinPrice" class="form-control" step="0.01" />
            </div>
            <div class="col-md-3">
                <label>Max Price</label>
                <input type="number" asp-for="MaxPrice" class="form-control" step="0.01" />
            </div>
            <div class="col-md-3">
                <button type="submit" class="btn btn-primary">Filter</button>
            </div>
        </div>
    </form>
    
    @if (Model.Products?.Count > 0)
    {
        <table class="table">
            <thead>
                <tr>
                    <th>Product</th>
                    <th>Price</th>
                    <th>Stock</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var product in Model.Products)
                {
                    <tr>
                        <td>@product.ProductName</td>
                        <td>$@product.UnitPrice.ToString("F2")</td>
                        <td>@product.UnitsInStock</td>
                    </tr>
                }
            </tbody>
        </table>
    }
</div>
```

---

## ?? Troubleshooting

### Common Issues & Solutions

#### Issue 1: "Cannot open database connection"
```
Error: The server was not found or was not accessible
```

**Solution**:
1. Verify SQL Server is running
2. Check connection string in appsettings.json
3. Test connection in SSMS
4. Ensure firewall allows SQL port 1433
```bash
# Verify SQL Server is running (Windows)
Get-Service MSSQLSERVER

# Start if stopped
Start-Service MSSQLSERVER
```

#### Issue 2: "Cannot find stored procedure"
```
Error: Procedure or function 'dbo.GetAllProducts' not found
```

**Solution**:
1. Run SQL scripts to create stored procedures
2. Verify stored procedure names match in code
3. Check procedure schema (should be `dbo.`)
4. Re-execute create procedure script

```sql
-- Verify procedures exist
SELECT * FROM sys.procedures WHERE schema_id = 1;
```

#### Issue 3: "Timeout expired"
```
Error: Timeout expired. The timeout period elapsed prior to completion of the operation
```

**Solution**:
1. Increase command timeout in ProductService:
```csharp
command.CommandTimeout = 300; // 5 minutes
```

2. Optimize SQL query performance
3. Check for table locks:
```sql
sp_who2; -- See active connections
```

#### Issue 4: "Model validation failed"
```
Error: The value is invalid for the property
```

**Solution**:
1. Check model property types match database
2. Enable client-side validation
3. Add validation attributes to model:
```csharp
public class Product
{
    [Required]
    public string ProductName { get; set; }
    
    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}
```

#### Issue 5: "Null reference exception on async operation"
```
Error: Object reference not set to an instance of an object
```

**Solution**:
1. Always use `await` on async methods
2. Don't use `.Result` (causes deadlock)
3. Check null before accessing properties
```csharp
// ? Wrong
var result = _service.GetProductAsync().Result;

// ? Correct
var result = await _service.GetProductAsync();
```

---

## ?? Additional Resources

### Microsoft Documentation
- [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages)
- [ADO.NET SqlConnection](https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection)
- [Async/Await](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming)
- [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

### Related Tools
- [SQL Server Documentation](https://learn.microsoft.com/en-us/sql/sql-server/)
- [SSMS Download](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [Visual Studio](https://visualstudio.microsoft.com/)

### Design Patterns Used
- **Repository Pattern** - Data abstraction
- **Service Pattern** - Business logic encapsulation
- **Dependency Injection** - Loose coupling
- **Async/Await Pattern** - Non-blocking I/O
- **SOLID Principles** - Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion

---

## ?? Security Best Practices

### Implemented
? **Parameterized Queries** - All SQL uses `@Parameter` binding  
? **Connection Pooling** - SqlConnection manages pool automatically  
? **Async Operations** - Non-blocking prevents resource exhaustion  
? **Model Validation** - Input validation in business layer  

### Recommended Enhancements
- [ ] Add HTTPS enforcement
- [ ] Implement authentication (OAuth2, OpenID Connect)
- [ ] Add authorization (role-based access control)
- [ ] Enable CORS if exposing as API
- [ ] Add rate limiting
- [ ] Implement logging and monitoring
- [ ] Use secrets management (Azure Key Vault)
- [ ] Enable database encryption (TDE)
- [ ] Add API versioning
- [ ] Implement audit logging

---

## ?? License

This project is provided as-is for educational and commercial use.

---

## ?? Support & Contributing

### Reporting Issues
1. Check existing issues in repository
2. Provide detailed error messages
3. Include system information
4. Include steps to reproduce

### Contributing
1. Fork the repository
2. Create feature branch: `git checkout -b feature/YourFeature`
3. Commit changes: `git commit -m "Add YourFeature"`
4. Push to branch: `git push origin feature/YourFeature`
5. Submit pull request

---

## ?? Contact & Questions

For questions or issues:
- Create an issue in the GitHub repository
- Check existing documentation
- Review troubleshooting section

---

**Last Updated**: 2024  
**Version**: 1.0.0  
**Status**: Production Ready

