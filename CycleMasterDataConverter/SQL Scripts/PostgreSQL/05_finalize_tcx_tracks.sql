WITH final_tcx AS (
SELECT ride_id, '<?xml version="1.0" encoding="UTF-8"?>' || E'\r\n' ||
			xmlelement(name "TrainingCenterDatabase",
							xmlattributes('http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2 http://www.garmin.com/xmlschemas/TrainingCenterDatabasev2.xsd' as "xsi:schemaLocation",
										  'http://www.garmin.com/xmlschemas/ActivityGoals/v1' as "xmlns:ns5",
										  'http://www.garmin.com/xmlschemas/ActivityExtension/v2' as "xmlns:ns3",
										  'http://www.garmin.com/xmlschemas/UserProfile/v2' as "xmlns:ns2",
										  'http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2' as "xmlns",
										  'http://www.w3.org/2001/XMLSchema-instance' as "xmlns:xsi",
										  'http://www.garmin.com/xmlschemas/ProfileExtension/v1' as "xmlns:ns4"),
				 xmlconcat(xmlelement(name "Activities",
				  xmlelement(name "Activity", xmlattributes('Biking' as "Sport"),
							 xmlelement(name "Id", start_time_utc_gpx),
							 tcx,
							 xmlelement(name "Notes", 'Title: ' || title || '; ' || 'Notes: ' || extra_notes || '; ' || 'Course Joy: ' || course_joy || '; ' || 'Weather: ' || cmw.type),
							 xmlelement(name "Creator", xmlattributes('Device_t' as "xsi:type"),
										xmlelement(name "Name", 'CycleMaster'),
										xmlelement(name "Version",
												   xmlelement(name "VersionMajor", '3'),
												   xmlelement(name "VersionMinor", '2'),
												   xmlelement(name "BuildMajor", '0'),
												   xmlelement(name "BuildMinor", '0')
												  )
									   )
							)
				 ),
				 xmlelement(name "Authtor", xmlattributes('Application_t' as "xsi:type"),
							xmlelement(name "Name", 'CycleMaster'),
							xmlelement(name "Build",
									   xmlelement(name "Version",
												  xmlelement(name "VersionMajor", '3'),
												  xmlelement(name "VersionMinor", '2'),
												  xmlelement(name "BuildMajor", '0'),
												  xmlelement(name "BuildMinor" ,'0')
												 )
									  ),
							xmlelement(name "LangID", 'en')
						   )
				)
			)::text AS tcx
FROM cyclemaster.ridemetadata cmm, cyclemaster.rideweather cmw
WHERE cmm.weather = cmw.key
)


UPDATE cyclemaster.ridemetadata SET tcx_final = ft.tcx
FROM final_tcx ft
WHERE cyclemaster.ridemetadata.ride_id = ft.ride_id;
