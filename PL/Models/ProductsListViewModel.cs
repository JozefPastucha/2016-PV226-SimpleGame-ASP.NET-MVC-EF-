using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Products;

namespace PL.Models
{
    public class ProductsListViewModel
    {
        public int VillageId { get; set; }  //viem, ze aj produkty si to pamataju
        public List<ProductDTO> Products { get; set; }
    }
}