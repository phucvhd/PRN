using System;
using System.Collections.Generic;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class Product
    {
        public Product()
        {
            TblSuppliersReports = new HashSet<SuppliersReport>();
        }

        public string ProductId { get; set; }
        public string Isbn10 { get; set; }
        public string Isbn13 { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Image { get; set; }
        public string ForAgesId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Category Category { get; set; }
        public virtual Age ForAges { get; set; }
        public virtual ICollection<SuppliersReport> TblSuppliersReports { get; set; }
    }
}
