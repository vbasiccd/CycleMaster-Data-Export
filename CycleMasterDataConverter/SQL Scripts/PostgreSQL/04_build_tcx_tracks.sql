DROP TABLE IF EXISTS cyclemaster.ride_lap;
DROP TABLE IF EXISTS cyclemaster.combine_tracks;

CREATE TABLE cyclemaster.combine_tracks AS (
	SELECT ride_id, xmlelement(name "Track", xmlagg(tcx ORDER BY point_id ASC)) AS tcx
	FROM cyclemaster.ridedata
	GROUP BY ride_id);

CREATE TABLE cyclemaster.ride_lap AS (
	SELECT ct.ride_id, xmlelement(name "Lap", xmlattributes(cm.start_time_utc_gpx as "StartTime"),
										xmlelement(name "TotalTimeSeconds", cm.duration_seconds + cm.pause_duration_seconds),
										xmlelement(name "DistanceMeters", cm.distance),
										xmlelement(name "MaximumSpeed", cm.max_speed),
										xmlelement(name "Calories", cm.total_calories_burned),
										xmlelement(name "Intensity", 'Active'),
										xmlelement(name "TriggerMethod", 'Manual'),
										ct.tcx
									   ) AS tcx
	FROM cyclemaster.combine_tracks ct, cyclemaster.ridemetadata cm
	WHERE ct.ride_id = cm.ride_id
);


UPDATE cyclemaster.ridemetadata SET tcx = rl.tcx
FROM cyclemaster.ride_lap rl
WHERE cyclemaster.ridemetadata.ride_id = rl.ride_id;


DROP TABLE cyclemaster.ride_lap;
DROP TABLE cyclemaster.combine_tracks;
