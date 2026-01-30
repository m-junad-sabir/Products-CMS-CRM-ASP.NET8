namespace ProductsProjectCRUD.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; } // <--- Add this property
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } // <--- Add this property
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public DateTime DateAdded {  get; set; }
        public DateTime LastModified { get; set; }

    }
}
