using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Common;
using BL.DTOs.Filters;
namespace BL.DTOs.Village
{
    public class VillageListQueryResultDTO : PagedListQueryResultDTO<VillageDTO>
    {
        public PlayerVillageFilter Filter { get; set; }
    }
}
