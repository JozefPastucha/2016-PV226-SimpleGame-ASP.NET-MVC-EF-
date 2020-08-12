using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Web.Http;
using BL.DTOs.Players;
using BL.Facades;
using Newtonsoft.Json;
using X.PagedList;

namespace API.Controllers
{
    public class PlayersController : ApiController
    {
        // GET: Players
        public PlayerFacade PlayerFacade { get; set; }

       /* public IEnumerable<PlayerDTO> Get()
        {
            return PlayerFacade.ListPlayers();            
        }*/

        [Route("~/api/Players/{id}")]
        public PlayerDTO Get(int id)
        {
            return id <= 0 ? null : PlayerFacade.GetPlayer(id);
        }
    }
}