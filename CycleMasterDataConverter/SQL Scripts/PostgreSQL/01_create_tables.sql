DROP TABLE IF EXISTS cyclemaster.ridepauses;
DROP TABLE IF EXISTS cyclemaster.ridedata;
DROP TABLE IF EXISTS cyclemaster.ridemetadata;
DROP TABLE IF EXISTS cyclemaster.rideweather;

DROP SCHEMA IF EXISTS cyclemaster;


CREATE SCHEMA cyclemaster;

CREATE TABLE cyclemaster.rideweather (
	key INTEGER NOT NULL CONSTRAINT weather_pk PRIMARY KEY,
	type VARCHAR(20),
	UNIQUE(key)
	);

CREATE TABLE cyclemaster.ridemetadata (
	ride_id VARCHAR(20) NOT NULL CONSTRAINT meta_pk PRIMARY KEY,
	title VARCHAR(50),
	distance NUMERIC,
	start_time_utc TIMESTAMP WITHOUT TIME ZONE,
	start_time_utc_gpx VARCHAR(30),
	end_time_utc TIMESTAMP WITHOUT TIME ZONE,
	end_time_utc_gpx VARCHAR(30),
	duration_seconds INTEGER,
	date_and_utc_time TIMESTAMP WITHOUT TIME ZONE,
	date_and_utc_time_gpx VARCHAR(30),
	total_calories_burned INTEGER,
	max_speed NUMERIC,
	weather INTEGER REFERENCES cyclemaster.rideweather (key) ON DELETE RESTRICT ON UPDATE CASCADE,
	course_joy INTEGER,
	extra_notes VARCHAR(500),
	pause_duration_seconds INTEGER,
	gpx XML,
	gpx_final TEXT,
	tcx XML,
	tcx_final TEXT
	);

CREATE TABLE cyclemaster.ridepauses (
	id SERIAL NOT NULL CONSTRAINT pauses_pk PRIMARY KEY,
	ride_id VARCHAR(20) REFERENCES cyclemaster.ridemetadata (ride_id) ON DELETE RESTRICT ON UPDATE CASCADE,
	track_id INTEGER,
	pkey INTEGER,
	duration_seconds INTEGER,
	cumm_seconds INTEGER
	);

CREATE TABLE cyclemaster.ridedata (
	id BIGSERIAL NOT NULL CONSTRAINT data_pk PRIMARY KEY,
	ride_id VARCHAR(20) REFERENCES cyclemaster.ridemetadata (ride_id) ON DELETE RESTRICT ON UPDATE CASCADE,
	track_id INTEGER,
	point_id INTEGER,
	time_gathered_utc TIMESTAMP WITHOUT TIME ZONE,
	time_adjusted_utc TIMESTAMP WITHOUT TIME ZONE,
	time_adjusted_utc_gpx VARCHAR(30),
	latitude NUMERIC,
	longitude NUMERIC,
	altitude NUMERIC,
	horizontal_accuracy INTEGER,
	vertical_accuracy INTEGER,
	speed NUMERIC,
	course NUMERIC,
	distance NUMERIC,
	calories_burned NUMERIC,
	gpx XML,
	tcx XML
	);


INSERT INTO cyclemaster.rideweather (key, type)
VALUES(0,'Sunny'),
	(1,'Little Cloudy'),
	(2,'Cloudy'),
	(3,'Rainy'),
	(4,'Snowy'),
	(5,'Stormy');
