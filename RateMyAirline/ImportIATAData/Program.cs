using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ImportIATAData
{
    class Program
    {
        static String ConnStr = RateMyAirline.Globals.ConnStr;

        static void Main(string[] args)
        {
            MySqlConnection conn = new MySqlConnection(ConnStr);
            conn.Open();
            String command = "truncate table ratemyairline.iata_airport_info";
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.ExecuteNonQuery();

            String[] CSVFile = System.IO.File.ReadAllLines("airport-codes.csv");

            foreach (string s in CSVFile)
            {
                String clean_string = s.Replace("\"", "").Replace("'", "");
                string[] parts = clean_string.Split(',');

                int elevation = 0;
                double latitude = 0.0, longitude = 0.0;

                try
                {
                    string ident = parts[0];
                    string the_type = parts[1];
                    string name = parts[2];
                    double.TryParse(parts[4], out longitude);
                    double.TryParse(parts[3], out latitude);
                    Int32.TryParse(parts[5], out elevation);
                    string continent = parts[6];
                    string iso_country = parts[7];
                    string iso_region = parts[8];
                    string municipality = parts[9];
                    string gps_code = parts[10];
                    string iata_code = parts[11];
                    string local_code = parts[12];

                    string insert_command_str = "INSERT into RateMyAirline.iata_airport_info (" +
                        "dent,type,name,latitude,longitude,elevation,continent,iso_country,iso_region,municipality,gps_code,iata_code,local_code)" +
                            "values (" + "\'" + ident + "\'" + ","
                                    + "\'" + the_type + "\'" + ","
                                    + "\'" + name + "\'" + ","
                                     + longitude.ToString() + ","
                                      +  latitude.ToString()  + ","
                                       +  elevation.ToString() + ","
                                        + "\'" + continent + "\'" + ","
                                         + "\'" + iso_country + "\'" + ","
                                          + "\'" + iso_region + "\'" + ","
                                           + "\'" + municipality + "\'" + ","
                                            + "\'" + gps_code + "\'" + ","
                                             + "\'" + iata_code + "\'" + ","
                                              + "\'" + local_code + "\'" + ")";

                    MySqlCommand insert_command = new MySqlCommand(insert_command_str, conn);

                    try
                    {
                        insert_command.ExecuteNonQuery();
                    }

                    catch(Exception ex)
                    {
                        System.Console.WriteLine(ex.ToString());
                        System.Console.WriteLine("COMMAND WAS: " + insert_command_str);
                    }
                }

                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
            conn.Close();
        }
    }
}
