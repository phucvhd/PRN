using System;
using System.Collections.Generic;

#nullable disable

namespace BookManageLibrary.DataAccess
{
    public partial class Category
    {
        public Category()
        {
            TblProducts = new HashSet<Product>();
        }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> TblProducts { get; set; }
    }
}
