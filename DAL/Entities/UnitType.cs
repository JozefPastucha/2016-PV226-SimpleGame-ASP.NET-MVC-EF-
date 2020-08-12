using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;
using DAL.Enums;

namespace DAL.Entities
{
    public class UnitType : IEntity<int>
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int HP { get; set; }

        public int Damage { get; set; }

        public string Cost { get; set; }

        public UnitRole Role { get; set; }  //hostility
    }
}
