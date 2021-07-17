using System;
using System.Collections.Generic;
using System.Text;

namespace CycleLibrary
{
    public class XmlFile
    {
        public XmlFile()
        {
            // leave blank for XML serialization
        }


        public Metadata RideMetadata { get; set; }

        public TrackSegment RideTracks { get; set; }
    }


    public class Metadata
    {
        public Metadata()
        {
            // leave blank for XML serialization
        }


        public string RideID { get; set; }
        public string Title { get; set; }
        public double Distance { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public int DurationSeconds { get; set; }
        public DateTime RideDateTimeUtc { get; set; }
        public int TotalCaloriesBurned { get; set; }
        public double MaxSpeed { get; set; }
        public string Weather { get; set; }
        public int CourseJoy { get; set; }
        public string ExtraNotes { get; set; }
    }

    public class TrackSegment
    {
        public TrackSegment()
        {
            // leave blank for XML serialization
        }


        public TrackPoint SegmentPoints { get; set; }
    }

    public class TrackPoint
    {
        public TrackPoint()
        {
            // leave blank for XML serialization
        }


        //"ride_id,track_id,point_id,time_gathered_utc,time_adjusted_utc,time_adjusted_utc_gpx,latitude,longitude,altitude," +
                                //"horizontal_accuracy,vertical_accuracy,speed,course,distance,calories_burned"
    }
}
