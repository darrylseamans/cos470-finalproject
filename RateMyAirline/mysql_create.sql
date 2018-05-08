drop database if exists RateMyAirline;
create database RateMyAirline;
use RateMyAirline;
create table iata_airport_info(
id int(6) not null auto_increment primary key,
dent varchar(8),
type varchar(24),
name varchar(96),
latitude decimal(20,8),
longitude decimal(20,8),
elevation int(8),
continent varchar(96),
iso_country varchar(96),
iso_region varchar(96),
municipality varchar(96),
gps_code varchar(96),
iata_code varchar(96),
local_code varchar(96)
);

create table ratings
(
id int(6) not null auto_increment primary key,
airport_id int(6),
user_name varchar(64),
facilities int(2),
customer_service int(2),
parking int(2),
amenities int(2),
wait_time int(2),
comments varchar(256),
date_time int(12)
);

alter table iata_airport_info engine=MyISAM;
alter table ratings engine=MyISAM;
