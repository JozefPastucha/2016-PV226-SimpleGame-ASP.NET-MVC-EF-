using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using BL.DTOs.Buildings;
using PL.Models;
using BL.DTOs.AdventureTypes;
using BL.DTOs.Units;

namespace PL.Controllers
{
    public class AdventuresController : Controller
    {
        public BuildingFacade BuildingFacade { get; set; }
        public AdventureFacade AdventureFacade { get; set; }
        public UnitFacade UnitFacade { get; set; }
        public PlayerFacade PlayerFacade { get; set; }
        public VillageFacade VillageFacade { get; set; }

        // GET: Adventures
        public ActionResult Index(int id)
        {
            if (!BuildingFacade.GetBuildingByNameAndVillageId("Tavern", id).Built)
            {
                return RedirectToAction("VisitVillage", "Villages", new { id = id });
            }

            var result1 = new List<AdventureDTO>(AdventureFacade.ListAdventures(id));
            var result2  = new List<UnitDTO>(UnitFacade.ListUnitsByVillage(id));
            var model = new AdventureListViewModel() { Adventures = result1, Units = result2, VillageID = id};
           
            return View("AdventureListView", model);
        }

        public ActionResult ChooseUnits(AdventuresChooseUnitsModel model)
        {
            var units = new List<UnitDTO>(UnitFacade.ListUnitsByVillage(model.VillageID));
            model.UnitsTypes = new List<string>(units.Select(u => u.UnitType));
            model.UnitsCount = new List<int>(new int [units.Count()]);

            return View(model);
        }

        [HttpPost]
        [ActionName("ChooseUnits")]
        public ActionResult ChooseUnitsPost(AdventuresChooseUnitsModel model)
        {
            List <UnitDTO> units = new List<UnitDTO>(UnitFacade.ListUnitsByVillage(model.VillageID));
            for(int i = 0; i < units.Count(); ++i)
            {
                units[i].Count = model.UnitsCount[i];
            }
            if (ModelState.IsValid)
            {
                AdventureFacade.Adventure(units, model.AdventureTypeId);//esteze okej cakaj
            }
            return RedirectToAction("Index", "Adventures", new { id = model.VillageID });
        }

        public ActionResult SettleNewVillage(int id)
        {
            var player = PlayerFacade.GetPlayerAccordingToUserName(User.Identity.Name);

            VillageFacade.SettleNewVillage(id);
            return RedirectToAction("Index", "Villages");

        }
    }
}