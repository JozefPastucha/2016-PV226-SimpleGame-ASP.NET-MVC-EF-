using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using BL.DTOs.Village;
using BL.DTOs.Resources;
using BL.DTOs.Products;
using PL.Models;
using X.PagedList;


namespace PL.Controllers
{
    public class VillagesController : Controller
    {
        public VillageFacade VillageFacade { get; set; }
        public ResourceFacade ResourceFacade { get; set; }
        public ProductFacade ProductFacade { get; set; }
        public PlayerFacade PlayerFacade { get; set; }

        // GET: Villages
        public ActionResult Index(string UserName, int page = 1)
        { 
            var player = PlayerFacade.GetPlayerAccordingToUserName(UserName ?? User.Identity.Name);

            var result = VillageFacade.ListVillagesByPlayerPaging(player.ID, page);
            var model = new VillageListViewModel
            {
                Villages = new StaticPagedList<VillageDTO>(result.ResultsPage, result.RequestedPage, VillageFacade.VillagePageSize, result.TotalResultCount),
                PlayerId = player.ID
            };

            return View("VillageListView", model);
        }

        public ActionResult VisitVillage(int id)
        {
            var resources = new List<ResourceDTO>(ResourceFacade.ListResourcesByVillage(id));
            var products = new List<ProductDTO>(ProductFacade.ListProductsByVillage(id));

            var model = new VisitVillageViewModel { VillageId = id, Resources = resources, Products = products };
            return View("VisitVillageView", model);
        }
        
        public ActionResult AddResources()
        {
            var player = PlayerFacade.GetPlayerAccordingToUserName(User.Identity.Name);
            
            var villages = VillageFacade.ListVillagesByPlayer(player.ID); 

            foreach (var item in villages)
            {
                ResourceFacade.AddResources(new TimeSpan(0, 0, 1), item.ID);
            }

            return Content("Resources added");
        }
    }
}