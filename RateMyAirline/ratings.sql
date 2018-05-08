use RateMyAirline;
create table [dbo].[ratings]
(
id int identity(1,1) primary key,
airport_id int,
user_name varchar(64),
facilities int,
customer_service int,
parking int,
amenities int,
wait_time int,
comments varchar(max),
date_time datetime2)
;
