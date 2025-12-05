using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVarik3.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Status { get; set; } = "Обработка";
        public string DeliveryAddress { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? DeliveryDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
