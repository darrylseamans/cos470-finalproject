using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyAirline
{
    public class AirportData
    {
        public int id { get; set; }
        public String name { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public String region { get; set; }
        public string municipality { get; set; }
        public decimal diff_lat { get; set; }
        public decimal diff_long { get; set; }
        public decimal euclidan_distance { get; set; }

        public AirportData(string n, decimal lat, decimal longit, string r, string m, decimal difflat, decimal difflong, int the_id)
        {
            this.name = n; this.latitude = lat; this.longitude = longit; this.region = r; this.municipality = m; this.id = the_id;
            this.diff_lat = difflat; this.diff_long = difflong;

            double double_ecludian_distance = Math.Sqrt((double)(diff_lat * diff_lat) + (double)(diff_long * diff_long));
            euclidan_distance = (decimal)double_ecludian_distance;

        }
    }
}