using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Players
{
    public class PlayerDTO  
    {
        public int ID { get; set; }

        public string UserName { get; set; }
        
        public string Email { get; set; }

        public int NumOfVillages { get; set; }

    }
}
