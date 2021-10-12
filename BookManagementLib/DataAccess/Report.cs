using System;
using System.Collections.Generic;

#nullable disable

namespace BookManagementLib.DataAccess
{
    public partial class Report
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }
        public string UserEmail { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsReceiver { get; set; }

        public virtual Company Company { get; set; }
        public virtual Product Product { get; set; }
        public virtual User UserEmailNavigation { get; set; }
    }
}
