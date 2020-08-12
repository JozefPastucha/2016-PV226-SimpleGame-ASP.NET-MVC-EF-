using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Units;
using BL.Facades;
using PL.Models;

namespace PL.Controllers
{
    public class UnitsController : Controller
    {
        public UnitFacade UnitFacade { get; set; }
        public BuildingFacade BuildingFacade { get; set; }
        // GET: Units
        public ActionResult Index(int id)
        {
            if (!BuildingFacade.GetBuildingByNameAndVillageId("TrainingCamp", id).Built)
            {
                //w
                return RedirectToAction("VisitVillage", "Villages", new { id = id });
            }
            var result = new List<UnitDTO>(UnitFacade.ListUnitsByVillage(id));
            var model = new UnitsListViewModel { VillageId = id, Units = result };
            return View("UnitsListView", model);
        }

        public ActionResult TrainUnit(int id, int amount, int villageId)
        {
            UnitFacade.Train(id, amount);
            return RedirectToAction("Index", new { id = villageId });
        }
    }
}