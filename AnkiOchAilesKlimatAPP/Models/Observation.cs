﻿using System;
using System.Collections.Generic;
using System.Text;
using static AnkiOchAilesKlimatAPP.Repositories.ObserverRepository;

namespace AnkiOchAilesKlimatAPP.Models
{
   public class Observation
    {


        public  int Id { get; set; }
        public DateTime Date { get; set; }
        public int ObserverId { get; set; }
        public int GeolocationId {get; set; }


        public override string ToString() //ändrat strängens utseende med en basmetod 
        {
            return $"{(Date.ToString("yyyy/MM/dd"))}";
        }
    }
}
 