using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiOchAilesKlimatAPP.Models
{
    public class InformationDisplay
    {
       
        public string CountryName { get; set; }

        public string AreaName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Category { get; set; }

        public int BaseCategory { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }

        public int Measurement_id { get; set; }

        public string Abbrevation { get; set; }

        public string Type { get; set; }

        public override string ToString() 
        {
            return $"{Category}";
        }

    }
}
