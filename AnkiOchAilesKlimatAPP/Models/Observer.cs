using System;
using System.Collections.Generic;
using System.Text;

namespace AnkiOchAilesKlimatAPP.Models
{
    public class Observer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public override string ToString() //ändrat strängens utseende med en basmetod 
        {
            return $"{FirstName} {LastName} ";
        }
      
}    

}
