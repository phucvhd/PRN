using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.DataAccess
{
    public partial class Product
    {
        public Product()
        {
            Reports = new HashSet<Report>();
        }

        public string ProductId { get; set; }
        [StringLength(maximumLength: 10, ErrorMessage = "Length must be below 10")]
        public string Isbn10 { get; set; }
        [StringLength(maximumLength: 12, ErrorMessage = "Length must be below 12")]
        public string Isbn13 { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Product Name")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        public string ProductName { get; set; }
        [StringLength(maximumLength: 3000, ErrorMessage = "Length must be below 3000")]
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Price")]
        public decimal Price { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Quantity")]
        public int Quantity { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Author")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 50")]
        public string Author { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter the Publisher")]
        [StringLength(maximumLength: 10, MinimumLength = 2, ErrorMessage = "Length must be between 2 to 10")]
        public string Publisher { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please import the Image")]
        public string Image { get; set; }
        public string ForAgesId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Category Category { get; set; }
        public virtual Age ForAges { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
