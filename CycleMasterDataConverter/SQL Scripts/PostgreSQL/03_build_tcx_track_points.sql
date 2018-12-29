UPDATE cyclemaster.ridedata
	SET tcx = xmlelement(name "Trackpoint",
				  xmlelement(name "Time", time_adjusted_utc_gpx),
				  xmlelement(name "Position",
							 xmlelement(name "LatitudeDegrees", latitude),
							 xmlelement(name "LongitudeDegrees", longitude)
							),
				  xmlelement(name "AltitudeMeters", altitude),
				  xmlelement(name "DistanceMeters", distance),
				  xmlelement(name "Extensions",
							 xmlelement(name "ns3:TPX",
										xmlelement(name "ns3:Speed", speed)
									   )
							)
				  );
