using System;
using System.Collections.Generic;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class Age
    {
        public Age()
        {
            TblProducts = new HashSet<Product>();
        }

        public string ForAgesId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> TblProducts { get; set; }
    }
}
