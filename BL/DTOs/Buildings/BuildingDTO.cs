using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Buildings
{
    public class BuildingDTO    
    {
        public int ID { get; set; }
        public string BuildingType { get; set; }  
        public string ResourceType { get; set; }    //pre addresources 
        public int VillageID { get; set; }  //pre navrat do dediny z buldings
        public string Cost { get; set; }    //z b_type
        public int WorkersAssigned { get; set; } 
        public int ProductionPerWorker { get; set; }
        public int Production { get; set; }
        public bool Built { get; set; }
    }
}
