using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVarik3.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        public string Manufacturer { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? Discount { get; set; }

        public int StockQuantity { get; set; }

        public byte[]? ImageData { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual Supplier Supplier { get; set; } = null!;
    }
}
