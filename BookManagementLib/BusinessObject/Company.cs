using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.BusinessObject
{
    public partial class Company
    {
        public Company()
        {
            Reports = new HashSet<Report>();
        }

        public string CompanyId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Company Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        public string CompanyName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Company Email")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        public string CompanyEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Company Address")]
        [StringLength(maximumLength: 500, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 500")]
        public string CompanyAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Company Phone")]
        //[StringLength(maximumLength: 12, ErrorMessage = "Length must be below 12")]
        [Phone]
        public string CompanyPhone { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsReceiver { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
