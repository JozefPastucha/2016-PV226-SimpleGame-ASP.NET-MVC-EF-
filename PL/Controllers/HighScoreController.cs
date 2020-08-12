using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Players;
using BL.Facades;
using X.PagedList;

namespace PL.Controllers
{
    public class HighScoreController : Controller
    {
        public PlayerFacade PlayerFacade { get; set; }
        // GET: HighScore
        public ActionResult Index(int page = 1)
        {
            var result = PlayerFacade.ListPlayersPaging("NumOfVillages", page);
            var model = new StaticPagedList<PlayerDTO>(result.ResultsPage, result.RequestedPage, PlayerFacade.PlayerPageSize, result.TotalResultCount);
            return View(model);
        }
    }
}