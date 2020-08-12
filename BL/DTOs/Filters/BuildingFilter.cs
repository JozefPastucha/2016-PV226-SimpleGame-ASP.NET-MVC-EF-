using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Filters
{
    public class BuildingFilter
    {
        public int VillageId { get; set; }
        public bool WorkersAssigned { get; set; }
        public string BuildingType { get; set; }
    }
}
