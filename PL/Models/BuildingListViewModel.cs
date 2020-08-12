using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Buildings;


namespace PL.Models
{
    public class BuildingListViewModel
    {
        public int VillageId { get; set; }  //viem, ze aj budovy si to pamataju
        public List<BuildingDTO> Buildings { get; set; }
        public int Huts { get; set; }
        public string HutCost { get; set; }     
    }
}