using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs.Units
{
    public class UnitDTO        //toto ked idem hore, ale uz ked som hore, napr pri adventures tak budem chciet ist dole, na adventureService, kde potrebujem vediet co je to tank/ tak ale to si mozem podla mna zistit aj z databazy ked uz som v BL, aj tak to musim najst v databaze ked to budem menit, ci?
    {
        public int ID { get; set; }///
        public string UnitType { get; set; }    //to budem vypisovat // a este podla ohonajdes v databaze ked ides do adventureServ
        public int VillageID { get; set; }      //pre navrat z view Training naspat do dediny
        public int HP { get; set; } //pri Train a taktiez ked ide do service tak podla tohoto zoradit//A TIEZ POUZIVAM TOTO HP NAKONIEC
        public int Damage { get; set; } //pri Train     //nakoniec ked idem dole tak pouzivam tento dmg

        public string Cost { get; set; }    //pri Train
        public int Count { get; set; }  //kolko ich mas pri Train a ADventure a potom do toho nastavis kolko ich chces poslat na vypravu
    }
}
