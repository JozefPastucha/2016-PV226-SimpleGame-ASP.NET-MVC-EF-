using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.AdventureTypes
{
    public class AdventureDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Enemy { get; set; }
        public int EnemyCount { get; set; }
        public int BreadPerUnit { get; set; }
        public string ResourcesReward { get; set; }
        public string ProductsReward { get; set; }
        public bool Accomplished { get; set; }  //v PL zistim, ci bol drak zabity -> mozme new village
       
    }
}
