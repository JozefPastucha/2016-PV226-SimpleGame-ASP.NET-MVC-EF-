using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Products;

namespace BL.Services.Products
{
    public interface IProductService
    {
        /// <summary>
        /// Produces a product
        /// </summary>
        /// <param name="productId">product id</param>
        /// <param name="amount">amount of products to be produces</param>
        /// <returns>True if there is enough resources in the village, false otherwise</returns>
        bool Produce(int productId, int amount);

        /// <summary>
        /// Lists all product of a village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>List of product in the village</returns>
        IEnumerable<ProductDTO> ListProductsByVillage(int villageId);
    }
}
