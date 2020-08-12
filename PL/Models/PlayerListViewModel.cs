using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Players;
using X.PagedList;

namespace PL.Models
{
    public class PlayerListViewModel
    {
        public IPagedList<PlayerDTO> Players { get; set; }
    }
}