using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.AdventureTypes;
using BL.DTOs.Units;

namespace PL.Models
{
    public class AdventureListViewModel
    {
        public int VillageID { get; set; }  //aj adventure a units si ju pamataju, ale takto je to prehladnejsie
        public List<AdventureDTO> Adventures { get; set; }
        public List<UnitDTO> Units { get; set; }
    }
}