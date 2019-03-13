using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.ServiceModel
{
    public class Menu_Definition_Config
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int MenuId { get; set; }
        public int IngredientId { get; set; }
        public float Quantity { get; set; }
        public int UnitId { get; set; }
        public string Description { get; set; }
        public int? Status { get; set; }
    }
}
