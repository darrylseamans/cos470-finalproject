/* (43.6591, -70.2568) are the latitude and longitude for Portland, ME
 * These numbers are examples -- the real numbers will be pulled from the device GPS
 *
 * 2.0 is the maximum distance in degrees latitude and longitude for an airport to be considerd "nearby"
 * This value can be reduced to bring up fewer hits.
 *
 * The results will be sorted by distance (using a Euclidean distance formula or something similar) by
 * application logic between the database and client (mobile device) to produce a list of closest airports first
 */

SELECT  [name]
      ,[latitude]
      ,[longitude]
      ,[iso_country]
      ,[iso_region]
      ,[municipality]
      ,[type]
	  ,(abs(latitude - 43.6591)) as lat_diff
	  ,(abs(longitude - (-70.2568))) as long_diff
	 FROM [RateMyAirline].[dbo].[iata_airport_info]
  
	 where (abs(latitude - 43.6591)) < 2.0 /* max degrees difference */
		and (abs(longitude - (-70.2568))) < 2.0 /* max degrees difference */
		and type = 'large_airport'

  order by name