using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Enums;

namespace BL.DTOs.UnitTypes
{
    public class UnitTypeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }  
        public int HP { get; set; } //pre query z DAL do BL pri ADVEntures
        public int Damage { get; set; }//pre query z DAL do BL pri ADVEntures
        public UnitRole Role { get; set; }
    }
}
