using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BookManagementLib.DataAccess
{
    public partial class User
    {
        public User()
        {
            Reports = new HashSet<Report>();
        }
        
        public string Email { get; set; }
        
        public string FullName { get; set; }
        
        public string PhoneNum { get; set; }
        
        public string Password { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
