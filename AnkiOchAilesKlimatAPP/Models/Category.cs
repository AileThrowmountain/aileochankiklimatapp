using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiOchAilesKlimatAPP.Models
{
   public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BaseCategoryId { get; set; }
        public int UnitId { get; set; }
    }
}
