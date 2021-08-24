using System;
using System.Collections.Generic;
using System.Text;

namespace CycleLibrary
{
    public class CsvFiles
    {
        private string _metadataPath;
        private string _ridedataPath;

        public CsvFiles(string baseFolderPath)
        {
            _metadataPath = FileIO.MergeTwoPathParts(baseFolderPath, "metadata.csv");
            _ridedataPath = FileIO.MergeTwoPathParts(baseFolderPath, "ridedata.csv");

            WriteCsvHeaders();
        }

        public void PopulateCSVs(ref XmlFile rideFile)
        {
            // write the metadata
            FileIO.WriteTextFile($"{rideFile.RideMetadata.RideID},{rideFile.RideMetadata.Title},{rideFile.RideMetadata.Distance}," +
                                    $"{DateTimeToDatabaseString(rideFile.RideMetadata.StartTimeLocal)},{DateTimeToDatabaseString(rideFile.RideMetadata.StartTimeUtc)},{rideFile.RideMetadata.StartTimeUtcString}," +
                                    $"{DateTimeToDatabaseString(rideFile.RideMetadata.EndTimeLocal)},{DateTimeToDatabaseString(rideFile.RideMetadata.EndTimeUtc)},{rideFile.RideMetadata.EndTimeUtcString}," +
                                    $"{rideFile.RideMetadata.DurationSeconds},{rideFile.RideMetadata.PauseSeconds}," +
                                    $"{DateTimeToDatabaseString(rideFile.RideMetadata.RideDateTimeLocal)},{DateTimeToDatabaseString(rideFile.RideMetadata.RideDateTimeUtc)},{rideFile.RideMetadata.RideDateTimeUtcString}," +
                                    $"{rideFile.RideMetadata.TotalCaloriesBurned},{rideFile.RideMetadata.MaxSpeed},{rideFile.RideMetadata.Weather}," +
                                    $"{rideFile.RideMetadata.CourseJoy},{rideFile.RideMetadata.ExtraNotes}",
                                    _metadataPath, isAppend: true);

            // write the ride data
            for (int t = 0; t < rideFile.RideTracks.Count; t++)
            {
                for (int p = 0; p < rideFile.RideTracks[t].SegmentPoints.Count; p++)
                {
                    TrackPoint currentPoint = rideFile.RideTracks[t].SegmentPoints[p];

                    FileIO.WriteTextFile($"{rideFile.RideMetadata.RideID},{t + 1},{p + 1}," +
                                        $"{DateTimeToDatabaseString(currentPoint.PointTimeLocal)},{DateTimeToDatabaseString(currentPoint.PointTimeUtc)},{currentPoint.PointTimeUtcString}," +
                                        $"{currentPoint.Latitude},{currentPoint.Longitude},{currentPoint.Altitude},{currentPoint.HorizontalAccuracy},{currentPoint.VerticalAccuracy}," +
                                        $"{currentPoint.Speed},{currentPoint.Course},{currentPoint.Distance},{currentPoint.CaloriesBurned}",
                                        _ridedataPath, isAppend: true);
                }
            }

        }

        private void WriteCsvHeaders()
        {
            FileIO.WriteTextFile("ride_id,title,distance,start_time_local,start_time_utc,start_time_utc_gpx,end_time_local,end_time_utc,end_time_utc_gpx," +
                                    "duration_seconds,pause_seconds,ride_date_time_local,ride_date_time_utc,ride_date_time_utc_gpx,total_calories_burned," +
                                    "max_speed,weather,course_joy,extra_notes",
                                    _metadataPath);

            FileIO.WriteTextFile("ride_id,track_id,point_id,point_time_local,point_time_utc,point_time_utc_gpx,latitude,longitude,altitude," +
                                    "horizontal_accuracy,vertical_accuracy,speed,course,distance,calories_burned",
                                    _ridedataPath);
        }

        private string DateTimeToDatabaseString(DateTime timestamp)
        {
            return timestamp.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
