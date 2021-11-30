using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.BusinessObject
{
    public partial class User
    {
        public User()
        {
            Reports = new HashSet<Report>();
        }
        [Key]
        public string Email { get; set; }
        
        public string FullName { get; set; }
        
        public string PhoneNum { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 6, ErrorMessage = "Length must be between 6 to 30")]
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
