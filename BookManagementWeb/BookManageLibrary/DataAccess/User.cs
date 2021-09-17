using System;
using System.Collections.Generic;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class User
    {
        public User()
        {
            TblSuppliersReports = new HashSet<SuppliersReport>();
        }

        public string Email { get; set; }
        public string FullName { get; set; }
        public string PhoneNum { get; set; }
        public string Password { get; set; }

        public virtual ICollection<SuppliersReport> TblSuppliersReports { get; set; }
    }
}
