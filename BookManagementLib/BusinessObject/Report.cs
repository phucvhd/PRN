using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.BusinessObject
{
    public partial class Report
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string ProductId { get; set; }
        public DateTime CreatedDate { get; set; }
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Report Quantity")]
        public int Quantity { get; set; }
        public string UserEmail { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsReceiver { get; set; }

        public virtual Company Company { get; set; }
        public virtual Product Product { get; set; }
        public virtual User UserEmailNavigation { get; set; }
    }
}
