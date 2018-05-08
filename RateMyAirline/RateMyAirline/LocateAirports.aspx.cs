using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace RateMyAirline
{
    public partial class LocateAirports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = GetAirports();
            this.lblResults.Text = s;
        }


        protected string GetAirports()
        {
            String ConnStr = Globals.ConnStr;
            double close_by = 2.0;
            string LatString = Request.QueryString["lat"];
            string LonString = Request.QueryString["lon"];
            List<AirportData> data = new List<AirportData>();

            double lat, lon;

            double.TryParse(LatString, out lat);
            double.TryParse(LonString, out lon);


            MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();

            String query = @"SELECT  name,latitude,longitude,iso_country,iso_region,municipality,type,(abs(latitude - "
    + lat.ToString() + " )) as lat_diff,(abs(longitude - (" + lon.ToString() + "))) as long_diff, id FROM RateMyAirline.iata_airport_info "
    + "where (abs(latitude -" + lat.ToString() + ")) < " + close_by.ToString() + " and (abs(longitude - " + lon.ToString() + ")) < " + close_by.ToString()
    + " and type = 'large_airport'  order by name";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            String s = "";

            while (reader.Read())
            {
                String name = reader.GetString(0);
                decimal latitude = reader.GetDecimal(1);
                decimal longitude = reader.GetDecimal(2);
                String region = reader.GetString(4);
                string municipality = reader.GetString(5);
                decimal diff_lat = reader.GetDecimal(7);
                decimal diff_long = reader.GetDecimal(8);
                int ID = reader.GetInt32(9);
                data.Add(new AirportData(name, latitude, longitude, region, municipality, diff_lat, diff_long, ID));
            }

            List<AirportData> sorted_list = (from d in data orderby d.euclidan_distance select d).ToList();

            // s = "<table border=\"1\" style = \"margin-left: auto; margin-right: auto; \">";

            int count_of = 0;
            foreach (AirportData d in sorted_list)
            {
                if (count_of < 1000000)
                {
                    //s += "<tr><td>" + d.id.ToString() + "</td><td><a href=/Moreinfo?id=" + d.id.ToString() + ">" + d.name.ToString() + "</a></td><td>" + d.latitude.ToString() + "," + d.longitude.ToString() + "</td><td>" + d.region.ToString() + "</td><td>" + d.municipality.ToString() + "</td><td>" + d.diff_lat.ToString() + "</td><td>" + d.diff_long.ToString() + "</td><td>" + d.euclidan_distance.ToString() + "</td><td><a href=/Ratings?id=" + d.id.ToString() + ">" + d.name.ToString() + "</a>" + "</td></tr>";
                    s += "|" + d.name.ToString();
                    ++count_of;
                }
            }

            reader.Close();
            conn.Close();
            return s;
        }
    }
}