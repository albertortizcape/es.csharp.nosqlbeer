//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoSQLBeerCore.SqlServer
{
    using System;
    using System.Collections.Generic;
    
    public partial class breweries
    {
        public breweries()
        {
            this.beers = new HashSet<beers>();
            this.breweries_geocode = new HashSet<breweries_geocode>();
        }
    
        public int ID { get; set; }
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string code { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public string descript { get; set; }
    
        public virtual ICollection<beers> beers { get; set; }
        public virtual ICollection<breweries_geocode> breweries_geocode { get; set; }
    }
}