using System;
using System.Collections.Generic;


/*
 * This is a basic XML schema that stores all the data for a trip
 * after extraction and conversion.
 * Each trip will be converted to this XML schema. The other conversions
 * will pull data from this converted file.
*/


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

        /// <summary>
        /// Generate a file name using the ride ID and provided extension.
        /// </summary>
        /// <param name="extensionNoDot">file extension without period</param>
        /// <returns>ride ID file name with extension</returns>
        public string GetFileName(string extensionNoDot)
        {
            string output;

            // to be safe, check if the period exists
            if (extensionNoDot.StartsWith("."))
            {
                output = extensionNoDot.Substring(1);
            }
            else
            {
                output = extensionNoDot;
            }

            output = output.Trim();

            output = $"{RideMetadata.RideID}.{output}";

            return output;
        }
    }


    /// <summary>
    /// Class to store the metadata for the trip.
    /// The "UtcString" variables stores the DateTimes as formatted strings
    /// needed for the GPX and TCX exports.
    /// The "CourseJoy" variable was a star rating in the phone app. Can't remember
    /// nor find if it was out of 4 or 5 stars.
    /// </summary>
    public class Metadata
    {
        public Metadata()
        {
            // leave blank for XML serialization
        }


        public string RideID { get; set; }
        public string Title { get; set; }
        public double Distance { get; set; }

        public DateTime StartTimeLocal { get; set; }
        public DateTime StartTimeUtc { get; set; }
        public string StartTimeUtcString { get; set; }

        public DateTime EndTimeLocal { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string EndTimeUtcString { get; set; }

        public int DurationSeconds { get; set; }
        public int PauseSeconds { get; set; }
        public int TotalSeconds { get => DurationSeconds + PauseSeconds; }

        public DateTime RideDateTimeLocal { get; set; }
        public DateTime RideDateTimeUtc { get; set; }
        public string RideDateTimeUtcString { get; set; }

        public int TotalCaloriesBurned { get; set; }
        public double MaxSpeed { get; set; }
        public string Weather { get; set; }
        public int CourseJoy { get; set; }
        public string ExtraNotes { get; set; }
    }

    /// <summary>
    /// Multiple track segments are possible if the trip was paused in the phone app.
    /// Otherwise, there will be only one track segment.
    /// </summary>
    public class TrackSegment
    {
        public TrackSegment()
        {
            // leave blank for XML serialization
        }


        public List<TrackPoint> SegmentPoints { get; set; }
    }

    /// <summary>
    /// Stores data for each GPS track point.
    /// The "UtcString" variable stores the DateTime as a formatted string,
    /// needed for the GPX and TCX exports.
    /// </summary>
    public class TrackPoint
    {
        public TrackPoint()
        {
            // leave blank for XML serialization
        }


        public DateTime PointTimeLocal { get; set; }
        public DateTime PointTimeUtc { get; set; }
        public string PointTimeUtcString { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double HorizontalAccuracy { get; set; }
        public double VerticalAccuracy { get; set; }
        public double Speed { get; set; }
        public double Course { get; set; }
        public double Distance { get; set; }
        public double CaloriesBurned { get; set; }
    }
}
