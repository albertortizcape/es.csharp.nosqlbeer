using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSQLBeerCore.Elements
{
    public class Beer
    {
        public int id { get; set; }
        public string name { get; set; }
        public double abv { get; set; }
        public string description { get; set; }
    }
}
