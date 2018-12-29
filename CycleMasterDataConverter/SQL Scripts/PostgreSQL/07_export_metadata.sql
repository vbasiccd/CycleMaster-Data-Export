SELECT ride_id, title, (start_time_utc at time zone 'UTC') AT time zone 'America/New_York' as start_time_local, total_calories_burned, round(distance*2.0/3219,3) as miles, round(max_speed*3125.0/1397,3) as max_mph, cmw.type as weather, course_joy, extra_notes || 'Recorded using the CycleMaster 3.2 app on my Nokia Lumia 925.' AS notes
FROM cyclemaster.ridemetadata cmm, cyclemaster.rideweather cmw
WHERE cmm.weather=cmw.key
ORDER BY date_and_utc_time ASC;
