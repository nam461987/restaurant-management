using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ServiceModel
{
    public class Menu_Config
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int BranchId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string UnitId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
