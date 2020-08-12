using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Resources;
using BL.DTOs.Products;

namespace PL.Models
{
    public class VisitVillageViewModel
    {
        public int VillageId { get; set; }
        public List<ResourceDTO> Resources { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}