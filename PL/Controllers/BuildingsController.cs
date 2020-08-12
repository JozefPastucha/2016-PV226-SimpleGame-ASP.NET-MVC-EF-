using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using PL.Models;
using BL.DTOs.Buildings;
using BL.DTOs.Resources;

namespace PL.Controllers
{
    public class BuildingsController : Controller
    {
        public BuildingFacade BuildingFacade { get; set; }
        // GET: Buildings
        public ActionResult Index(int id)
        {
            var result = new List<BuildingDTO>(BuildingFacade.ListBuildingsByVillage(id));
            
            int numOfHuts = BuildingFacade.GetNumOfHuts(id);
            var model = new BuildingListViewModel { VillageId = id, Buildings = result, Huts = numOfHuts, HutCost = String.Format("Wood : {0}, Stone {1}", numOfHuts * 100 + 100, numOfHuts * 60 + 60)};
            return View("BuildingListView", model);
        }

        public ActionResult BuildBuilding(BuildingDTO building)
        {
            BuildingFacade.BuildBuilding(building.ID);
            return RedirectToAction("Index", new { id = building.VillageID });
        }

        public ActionResult AddWorkers(BuildingDTO building)
        {
            BuildingFacade.AssignWorkers(building.ID, 1);
            return RedirectToAction("Index", new { id = building.VillageID });
        }
        public ActionResult RemoveWorkers(BuildingDTO building)
        {
            BuildingFacade.WithdrawWorkers(building.ID, 1);
            return RedirectToAction("Index", new { id = building.VillageID });
        }

        public ActionResult BuildHut(int id, int amount)
        {
            BuildingFacade.BuildHut(id, amount);
            return RedirectToAction("Index", new { id = id });
        }
    }
}