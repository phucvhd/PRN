using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.BusinessObject
{
    public partial class Age
    {
        public Age()
        {
            Products = new HashSet<Product>();
        }

        public string ForAgesId { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="Please enter the Description")]
        [StringLength(maximumLength:50,MinimumLength =2,ErrorMessage ="Length must be between 2 to 50")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
