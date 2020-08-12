using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Filters;
using BL.DTOs.Village;
using X.PagedList;  //IpagedList

namespace PL.Models
{
    public class VillageListViewModel
    {
        public int PlayerId { get; set; }

        public IPagedList<VillageDTO> Villages { get; set; }
    }
}