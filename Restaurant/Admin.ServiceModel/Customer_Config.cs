using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ServiceModel
{
    public class Customer_Config
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
