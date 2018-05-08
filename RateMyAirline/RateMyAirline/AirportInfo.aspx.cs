using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace RateMyAirline
{
    public partial class AirportInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string info = GetAirportInfo();
            this.lblResults.Text = info;
        }


        protected string GetAirportInfo()
        {
            String s = "";
            String ConnStr = Globals.ConnStr;
            double close_by = 2.0;
            int id = 0;
            int review_id = -1;
            String airport = Request.QueryString["airport"];
            MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();

            String query = "SELECT id,name FROM RateMyAirline.iata_airport_info where name = '" + airport + "'";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            s = "";

            while (reader.Read())
            {
                String name = reader.GetString(1);
                id = reader.GetInt32(0);
               // s += name;
            }
            reader.Close();

            query = "select id,airport_id,user_name,facilities,customer_service,parking,amenities,wait_time,comments,date_time from RateMyAirline.ratings where airport_id = " + id.ToString() + " order by date_time desc";

            cmd = new MySqlCommand(query, conn);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                review_id = reader.GetInt32(0);
                int review_airport_id = reader.GetInt32(1);
                string review_user_name = reader.GetString(2);
                int review_facilities = reader.GetInt32(3);
                int review_cust_service = reader.GetInt32(4);
                int review_parking = reader.GetInt32(5);
                int review_amenities = reader.GetInt32(6);
                int review_waittime = reader.GetInt32(7);
                string review_comments = reader.GetString(8);
                int time_of = reader.GetInt32(9);

                s += "|" + review_user_name + "," + review_facilities + "," + review_cust_service + "," + review_parking + "," + review_amenities + "," + review_waittime + "," + review_comments + "," + time_of.ToString();
            }

            conn.Close();
            return s;
        }
    }
}