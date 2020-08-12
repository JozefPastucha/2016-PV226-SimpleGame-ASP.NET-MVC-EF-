using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Products
{
    public class ProductDTO
    {
        public int ID { get; set; }
        public string ProductType { get; set; }
        public string BuildingType { get; set; }
        public string Cost { get; set; }
        public int Amount { get; set; } 
        public int VillageID { get; set; }
    }
}
