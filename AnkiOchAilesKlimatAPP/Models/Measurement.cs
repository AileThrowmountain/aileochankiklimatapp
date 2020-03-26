using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiOchAilesKlimatAPP.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public int ObservationId { get; set; }
        public int CategoryId { get; set; }
    }
}
