using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiOchAilesKlimatAPP.Models
{
   public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }


        public override string ToString() //ändrat strängens utseende med en basmetod 
        {
            return $"{CountryName}";
        }
    }
}
