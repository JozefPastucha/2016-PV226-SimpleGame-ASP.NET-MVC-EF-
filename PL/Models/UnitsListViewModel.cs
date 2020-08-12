using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Units;

namespace PL.Models
{
    public class UnitsListViewModel
    {
        public int VillageId { get; set; }  //viem, ze aj jednotky si to pamataju
        public List<UnitDTO> Units { get; set; }
    }
}