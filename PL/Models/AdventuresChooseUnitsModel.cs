using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.AdventureTypes;
using BL.DTOs.Units;

namespace PL.Models
{
    public class AdventuresChooseUnitsModel
    {
        public int VillageID { get; set; }
        public int AdventureTypeId { get; set; }
        public int BreadAmount { get; set; }
        public List<string> UnitsTypes { get; set; }
        public List<int> UnitsCount { get; set; }
    }
}