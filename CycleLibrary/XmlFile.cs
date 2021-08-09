using System;
using System.Collections.Generic;

namespace CycleLibrary
{
    public class XmlFile
    {
        public XmlFile()
        {
            // leave blank for XML serialization
        }


        public Metadata RideMetadata { get; set; }

        public List<TrackSegment> RideTracks { get; set; }
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


        public List<TrackPoint> SegmentPoints { get; set; }
    }

    public class TrackPoint
    {
        public TrackPoint()
        {
            // leave blank for XML serialization
        }


        public DateTime PointTimeUtc { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        // CONVERT THE ACCURACTY IN THE SERIALIZATION CLASS FROM INT TO DOUBLE
        public double HorizontalAccuracy { get; set; }
        public double VerticalAccuracy { get; set; }
        public double Speed { get; set; }
        public double Course { get; set; }
        public double Distance { get; set; }
        public double CaloriesBurned { get; set; }
    }
}
