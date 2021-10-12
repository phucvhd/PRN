using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.DataAccess
{
    public partial class Company
    {
        public Company()
        {
            Reports = new HashSet<Report>();
        }

        public string CompanyId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsReceiver { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
