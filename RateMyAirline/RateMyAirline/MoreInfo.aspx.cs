using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace RateMyAirline
{
    public partial class MoreInfo : System.Web.UI.Page
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
                conn.Close();
                this.lblAirport.Text = airport_name;

                for (int i = 1; i <= 5; ++i)
                {
                    this.cboCustomerService.Items.Add(i.ToString());
                    this.cboAmenities.Items.Add(i.ToString());
                    this.cboCleanliness.Items.Add(i.ToString());
                    this.cboWaitTime.Items.Add(i.ToString());
                    this.cboParking.Items.Add(i.ToString());
                   // this.cboParking.SelectedIndex = this.cboCustomerService.SelectedIndex = this.cboAmenities.SelectedIndex = this.cboCleanliness.SelectedIndex = this.cboWaitTime.SelectedIndex = 0;
                }
            }
        }

        protected void cmdSubmitReview_Click(object sender, EventArgs e)
        {
           // DateTime now = DateTime.Now;

            Int32 now  = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();

            String user_name = System.Web.HttpContext.Current.User.Identity.Name;
            int CustomerService = Int32.Parse(this.cboCustomerService.SelectedItem.Text.ToString());
            int Ameneties = Int32.Parse(this.cboAmenities.SelectedItem.Text.ToString());
            int Cleanliness = Int32.Parse(this.cboCleanliness.SelectedItem.Text.ToString());
            int WaitTime = Int32.Parse(this.cboWaitTime.SelectedItem.Text.ToString());
            int Parking = Int32.Parse(this.cboParking.SelectedItem.Text.ToString());
            String Comments = this.txtComments.Text;

            String FindCmd = "select id from  RateMyAirline.ratings where user_name = '" + user_name + "' and airport_id = " + airport_id.ToString();

            MySqlCommand cmd = new MySqlCommand(FindCmd, conn);

            MySqlDataReader reader = cmd.ExecuteReader();

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
                String DelCmd = "delete from RateMyAirline.ratings where id = " + review_id;
                cmd = new MySqlCommand(DelCmd, conn);
                cmd.ExecuteNonQuery();
            }

            string AddCmd = "insert into RateMyAirline.ratings (airport_id,user_name ,facilities,customer_service,parking,amenities,wait_time,comments,date_time) values (" +
                airport_id.ToString() + "," + "'" + user_name + "'," + Cleanliness.ToString() + "," + CustomerService.ToString() + "," + Parking.ToString() + "," + Ameneties.ToString() + "," + WaitTime.ToString() + ",'" + Comments.ToString() + "','" + now + "')";

            cmd = new MySqlCommand(AddCmd, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

            Response.Redirect("~/Welcome.aspx");
        }
    }
}