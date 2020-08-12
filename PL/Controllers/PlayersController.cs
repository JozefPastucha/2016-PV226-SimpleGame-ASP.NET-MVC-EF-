using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using PL.Models;
using X.PagedList;
using BL.DTOs.Players;

namespace PL.Controllers
{
    public class PlayersController : Controller
    {
        public PlayerFacade PlayerFacade { get; set; }
        // GET: Players
        public ActionResult Index(int page = 1)
        {
            var result = PlayerFacade
                .ListPlayersPaging("ID", page);
            var model = new PlayerListViewModel
            {
                Players = new StaticPagedList<PlayerDTO>(result.ResultsPage, result.RequestedPage, PlayerFacade.PlayerPageSize, result.TotalResultCount),
            };
            return View("PlayerListView", model);
        }

        public ActionResult DeletePlayer(int id)
        {
            PlayerFacade.DeletePlayer(id);
            return RedirectToAction("Index");
        }
    }
}