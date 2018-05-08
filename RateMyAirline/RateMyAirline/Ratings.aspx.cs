using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql.Data.MySqlClient;

namespace RateMyAirline
{
    public partial class Ratings : System.Web.UI.Page
    {
        String ConnStr = Globals.ConnStr;

        String airport_name = "";
        int airport_id = 0;
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated) // if the user is already logged in
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                airport_name = "";

                String id_string = Request.QueryString["id"];
                Int32.TryParse(id_string, out airport_id);
                this.lblAirport.Text = airport_id.ToString();

                MySqlConnection conn = new MySqlConnection(ConnStr);
                conn.Open();

                String query = @"SELECT name FROM RateMyAirline.iata_airport_info " + "where id = " + airport_id.ToString();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    airport_name = reader.GetString(0);
                }

                reader.Close();

                this.lblAirport.Text = airport_name;

                String user_name = System.Web.HttpContext.Current.User.Identity.Name;

                String FindCmd = "select id,airport_id,user_name,facilities,customer_service,parking,amenities,wait_time,comments,date_time from  RateMyAirline.ratings where airport_id = " + airport_id.ToString() + " order by date_time desc";
                cmd = new MySqlCommand(FindCmd, conn);
                reader = cmd.ExecuteReader();

                int review_id = -1;
                int count_of = 0;

                String result = "";
                result = "<table border=\"1\" style = \"margin-left: auto; margin-right: auto; \">";
                result += "<tr><th>Date</th><th>Name</th><th>Customer Service</th><th>Wait time</th><th>Amenities</th><th>Cleanliness</th><th>Parking</th></tr>";
                     
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

                    string item = "<tr><td>" + time_of.ToString() + "</td><td>" + review_user_name + "</td><td>" + review_cust_service.ToString() + "</td><td>"
                        + review_waittime.ToString() + "</td><td>" + review_amenities.ToString() + "</td><td>" + review_facilities.ToString() + "</td><td>"
                        + review_parking.ToString() + "</td></tr>";

                    result += item;
                    ++count_of;
                }

                result += "</table>";

                reader.Close();
                conn.Close();

                this.lblRatings.Text = result;
            }
        }

        protected void cmdFindAirports_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Welcome.aspx");
        }
    }
}