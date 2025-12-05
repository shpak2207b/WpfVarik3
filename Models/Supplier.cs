using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVarik3.Models
{
    public partial class Supplier
    {
        public int SupplierId { get; set; }

        public string SupplierName { get; set; } = null!;

        public string? ContactPhone { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
