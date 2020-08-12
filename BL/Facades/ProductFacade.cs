using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Products;
using BL.DTOs.Products;

namespace BL.Facades
{
    public class ProductFacade
    {
        private readonly IProductService productService;

        public ProductFacade(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Produces products (increases amount of this product in the village)
        /// </summary>
        /// <param name="productId">productId</param>
        /// <returns>True if there is enough resources in the village, false otherwise</returns>
        public bool Produce(int productId, int amount)
        {
            return productService.Produce(productId, amount);
        }

        /// <summary>
        /// Lists all product of a village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>List of product in the village</returns>
        public IEnumerable<ProductDTO> ListProductsByVillage(int villageId)
        {
            return productService.ListProductsByVillage(villageId);
        }
    }
}
