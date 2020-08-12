using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using BL.DTOs.Products;
using PL.Models;

namespace PL.Controllers
{
    public class ProductsController : Controller
    {
        public ProductFacade ProductFacade { get; set; }

        // GET: Products
        public ActionResult Index(int id)
        {
            var result = new List<ProductDTO>(ProductFacade.ListProductsByVillage(id));
            var model = new ProductsListViewModel { VillageId = id, Products = result };
            return View("ProductListView", model);
        }

        public ActionResult ProduceProduct(int id, int amount, int villageId)
        {
            ProductFacade.Produce(id, amount);
            return RedirectToAction("Index", new { id = villageId });
        }
    }
}