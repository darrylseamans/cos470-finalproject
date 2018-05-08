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
    public partial class SetAirportInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 now = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            MySqlConnection conn = new MySqlConnection(Globals.ConnStr);
            conn.Open();

            String cust_service = Request.QueryString["custservice"];
            String amenities = Request.QueryString["amenities"];
            String clean = Request.QueryString["cleanliness"];
            String wait_time = Request.QueryString["waittime"];
            String parking = Request.QueryString["parking"];
            String comments = Request.QueryString["comments"];
            String airport_name = Request.QueryString["airport_name"];

            if (cust_service == null)
                cust_service = "1";
            if (amenities == null)
                amenities = "2";
            if (clean == null)
                clean = "3";
            if (wait_time == null)
                wait_time = "4";
            if (parking == null)
                parking = "5";
            if (comments == null)
                comments = "Comment";
            if (airport_name == null)
                airport_name = "Portland International Jetport Airport";

            String user_name = Request.QueryString["user_name"];
            int CustomerService = Int32.Parse(cust_service);
            int Ameneties = Int32.Parse(amenities);
            int Cleanliness = Int32.Parse(clean);
            int WaitTime = Int32.Parse(wait_time);
            int Parking = Int32.Parse(parking);
            String Comments = comments;

            String FindIDCmd = "select id FROM RateMyAirline.iata_airport_info where name = '" + airport_name + "'";
            MySqlCommand cmd = new MySqlCommand(FindIDCmd, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            int airport_id = 0;

            while (reader.Read())
            {
                airport_id = reader.GetInt32(0);
            }

            reader.Close();

            /*
            String FindCmd = "select id from RateMyAirline.ratings where airport_id = " + airport_id.ToString();

            cmd = new MySqlCommand(FindCmd, conn);

            reader = cmd.ExecuteReader();

            int review_id = -1;
            int count_of = 0;

            while (reader.Read())
            {
                review_id = reader.GetInt32(0);
                ++count_of;
            }

            reader.Close();

            if (count_of != 0)
            {
                String DelCmd = "delete from [RateMyAirline].[dbo].[ratings] where id = " + review_id;
                cmd = new MySqlCommand(DelCmd, conn);
                cmd.ExecuteNonQuery();
            }
            */
            string AddCmd = "insert into RateMyAirline.ratings (user_name, airport_id, facilities,customer_service,parking,amenities,wait_time,comments,date_time) values (" + "'Anonymous'" + ","
              + airport_id.ToString() + "," + Cleanliness.ToString() + "," + CustomerService.ToString() + "," + Parking.ToString() + "," + Ameneties.ToString() + "," + WaitTime.ToString() + ",'" + Comments.ToString() + "','" + now + "')";

            cmd = new MySqlCommand(AddCmd, conn);
            cmd.ExecuteNonQuery();

            conn.Close();


        }
    }
}