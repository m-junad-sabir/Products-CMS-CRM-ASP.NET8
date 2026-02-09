# Products Management CMS/CRM Application

A professional **ASP.NET Core 8.0 Razor Pages** application for managing products, suppliers, and categories with CRUD operations.

---

## Table Of Content

- [Overview](#overview)

- [Key Features](#key-features)

- [Technology Stack](#technology-stack)

- [Screenshots - DEMO](#screenshots---demo)

- [Project Structure](#project-structure)

- [Prerequisites](#prerequisites)

- [Quick Start](#quick-start)

- [Running the Application](#running-the-application)

- [Architecture](#architecture)

- [Data Flow](#data-flow)

- [Database Schema](#database-schema)

- [Development Guidelines](#development-guidelines)

- [Adding Features](#adding-features)

- [Resources](#resources)

---

## Overview

Enables businesses to:

- Manage product catalogs with specifications
- Organize products by categories and suppliers
- Track inventory levels
- Perform CRUD operations
- Search and filter products

---

### Key Features

- Async/Await architecture for scalability
- 3-layer architecture (Presentation, Business Logic, Data Access)
- Stored procedures for optimal performance
- Dependency injection
- Bootstrap 5 responsive UI

---

## Technology Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| Framework | ASP.NET Core | 8.0 |
| UI Framework | Razor Pages | 8.0 |
| Data Access | ADO.NET | Microsoft.Data.SqlClient 6.0.2 |
| Database | SQL Server | 2019+ |
| Frontend | Bootstrap | 5.3.6 |

---

## Screenshots - DEMO

---

## Project Structure

```text
ProductsProjectCRUD/
├── Models/
│   ├── Product.cs, Category.cs, Supplier.cs
│   └── ProductService.cs (Data Access Layer)
├── BusinessLogicService/
│   └── ProductDataAccess.cs (Service Layer)
├── Pages/
│   ├── Products/ (CRUD pages)
│   └── ProductsByCategory/ (Filtering)
├── appsettings.json
└── Program.cs
```

---

## Prerequisites

- **Visual Studio 2022+** (ASP.NET workload)
- **.NET 8.0 SDK**
- **SQL Server 2019+**
- **Git**

---

## Quick Start

### 1. Clone Repository

```bash
git clone 
cd ProductsProjectCRUD
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure Database

Edit `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "ConnectionStringProducts": "Server=(localdb)\\mssqllocaldb;Database=ProductsDB;Integrated Security=true;Encrypt=false;"
  }
}
```

---

## Running the Application

### Visual Studio

1. Open solution
2. Press `F5` or Start Debugging
3. Navigate to `/Products`

### Command Line

```bash
cd ProductsProjectCRUD
dotnet run
```

Application available at `https://localhost:5001`

---

## Architecture

### 3-Tier Layers

**Presentation Layer** (`Pages/`)

- Razor Pages handle HTTP requests/responses
- Dependency injection of services

**Business Logic Layer** (`BusinessLogicService/ProductDataAccess.cs`)

- Input validation
- Business rule enforcement
- Service orchestration

**Data Access Layer** (`Models/ProductService.cs`)

- Execute stored procedures
- ADO.NET operations
- Connection management

### Data Flow

```text
Razor Page -> Business Logic -> Data Access -> SQL Server <- Data Access <- Business Logic <- Razor Page
```

---

## Database Schema

**Suppliers** (1:N) **Products** (N:1) **Categories**

- **Products**: ProductID, ProductName, SupplierID (FK), CategoryID (FK), UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, DateAdded, LastModified
- **Suppliers**: SupplierID, CompanyName, ContactName, Address, City, Region, Country, Phone, Email
- **Categories**: CategoryID, CategoryName, Description

---

## Development Guidelines

### Best Practices

- Always use `async/await` for I/O operations
- Use parameterized queries to prevent SQL injection
- Validate input in business logic layer
- Keep layers separate (don't mix concerns)
- Handle null checks properly

### Adding Features

1. Create stored procedure in SQL Server
2. Add method to `ProductService` (Data Access)
3. Add method to `ProductDataAccess` (Business Logic)
4. Create Razor Page and handler (Presentation)

---

## Resources

- [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages)
- [ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/)
- [SQL Server Docs](https://learn.microsoft.com/en-us/sql/sql-server/)
- [Async/Await](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming)

---

**Version**: 1.0.0 | **Status**: Production Ready
