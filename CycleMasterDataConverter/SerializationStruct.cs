using System;
using System.Collections.Generic;
//using System.Xml;
//using System.Xml.Serialization;

namespace CycleMasterDataConverter
{
    /// <summary>
    /// first Serializable class for GPS data file
    /// </summary>
    [Serializable()]
    public class SerializationStruct
    {
        // start time field and property
        private DateTime startValue;
        public DateTime startTime
        {
            get { return startValue; }
            set { startValue = value; }
        }

        // end time field and property
        private DateTime endValue;
        public DateTime endTime
        {
            get { return endValue; }
            set { endValue = value; }
        }

        // max speed field and property
        private double maxSpeedValue;
        public double dMaxSpeed
        {
            get { return maxSpeedValue; }
            set { maxSpeedValue = value; }
        }

        // GPS data list
        private List<PathCoord> listValue = new List<PathCoord>();
        public List<PathCoord> coordsList
        {
            get { return listValue; }
            set { listValue = value; }
        }

        // total or max length of ride pauses in ticks
        private long pauseValue;
        public long pauseDuration
        {
            get { return pauseValue; }
            set { pauseValue = value; }
        }

        // stores list of pauses during ride
        private List<item> pauseListValue = new List<item>();
        public List<item> pausesList
        {
            get { return pauseListValue; }
            set { pauseListValue = value; }
        }
    }

    /// <summary>
    /// stores pause list
    /// </summary>
    [Serializable()]
    public class item
    {
        public List<int> key { get; set; }
        public List<long> value { get; set; }
    }

    /// <summary>
    /// stores complete GPS path coordinates
    /// </summary>
    [Serializable()]
    public class PathCoord
    {
        private DateTime timeValue;
        public DateTime timeGathered
        {
            get { return timeValue; }
            set { timeValue = value; }
        }

        private coordUnit coordValues;
        public coordUnit coord
        {
            get { return coordValues; }
            set { coordValues = value; }
        }

        private double distValue;
        public double dWalkDistance
        {
            get { return distValue; }
            set { distValue = value; }
        }

        private double calorieValue;
        public double dCalories
        {
            get { return calorieValue; }
            set { calorieValue = value; }
        }
    }

    /// <summary>
    /// stores location data and an empty timestamp - possible future features?
    /// </summary>
    [Serializable()]
    public class coordUnit
    {
        // GPS location type field and property
        private LocationCoord locationValues;
        public LocationCoord Location
        {
            get { return locationValues; }
            set { locationValues = value; }
        }

        private List<int> timeValue;
        public List<int> Timestamp
        {
            get { return timeValue; }
            set { timeValue = value; }
        }
    }

    /// <summary>
    /// stores basic GPS location
    /// </summary>
    [Serializable()]
    public class LocationCoord
    {
        private double latValue;
        public double Latitude
        {
            get { return latValue; }
            set { latValue = value; }
        }

        private double lonValue;
        public double Longitude
        {
            get { return lonValue; }
            set { lonValue = value; }
        }

        private double altValue;
        public double Altitude
        {
            get { return altValue; }
            set { altValue = value; }
        }

        private int horizAcc;
        public int HorizontalAccuracy
        {
            get { return horizAcc; }
            set { horizAcc = value; }
        }

        private int vertAcc;
        public int VerticalAccuracy
        {
            get { return vertAcc; }
            set { vertAcc = value; }
        }

        private double speedValue;
        public double Speed
        {
            get { return speedValue; }
            set { speedValue = value; }
        }

        private double courseValue;
        public double Course
        {
            get { return courseValue; }
            set { courseValue = value; }
        }
    }
}
