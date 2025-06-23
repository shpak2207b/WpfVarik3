using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfVarik3.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int InventoryNumber { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateOnly PublicationDate { get; set; }
        public Status Status = new Status();
        public int? UserId { get; set; } 
        public User User { get; set; }
    }
}
