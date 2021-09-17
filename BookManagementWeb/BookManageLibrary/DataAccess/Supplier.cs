using System;
using System.Collections.Generic;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class Supplier
    {
        public Supplier()
        {
            TblSuppliersReports = new HashSet<SuppliersReport>();
        }

        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }

        public virtual ICollection<SuppliersReport> TblSuppliersReports { get; set; }
    }
}
