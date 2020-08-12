using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class Village : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public virtual Player Player { get; set; }

        public virtual List<Building> Buildings { get; set; }
        public virtual List<Unit> Units { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Resource> Resources { get; set; }

        public int Number { get; set; }

        public int Huts { get; set; }   

        public int AvailableWorkers { get; set; }    
       
    }
}
