using System;
using System.Collections.Generic;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class SuppliersReport
    {
        public int Id { get; set; }
        public string SuppilerId { get; set; }
        public string ProductId { get; set; }
        public DateTime SupplyDate { get; set; }
        public int SupplyQuantity { get; set; }
        public string ReceiverEmail { get; set; }

        public virtual Product Product { get; set; }
        public virtual User ReceiverEmailNavigation { get; set; }
        public virtual Supplier Suppiler { get; set; }
    }
}
