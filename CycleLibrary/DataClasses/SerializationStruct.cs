/*
 *  These are the serialization classes for the compressed ride data XML files,
 *  reverse engineered from my own GPS data.  I had used this Windows Phone app when
 *  it was titled "CycleMaster" back in 2014.
 *  
 *  NOTE:   I cannot guarantee 100% compatibility with your data!
 *          This app was later renamed by the author from "CycleMaster" to "RunMaster Cycle",
 *          and alongside this app was a Pedometer version, before both were discontinued.
 *          While reverse engineering my data files, I suspect that the author used the
 *          same XML schema for both apps, though I am unable to confirm that.
 *          If errors occur with your data conversion, message me and let's figure it out together.
*/

using System;
using System.Collections.Generic;

namespace CycleLibrary.DataClasses
{
    /// <summary>
    /// Root serialization class.
    /// </summary>
    [Serializable()]
    public partial class SerializationStruct
    {
        private DateTime _startTime;

        private DateTime _endTime;

        private double _dMaxSpeed;

        private List<PathCoord> _coordsList = new List<PathCoord>();

        private long _pauseDuration;

        private List<item> _pausesList = new List<item>();


        public DateTime startTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public DateTime endTime
        {
            get => _endTime;
            set => _endTime = value;
        }
        
        public double dMaxSpeed
        {
            get => _dMaxSpeed;
            set => _dMaxSpeed = value;
        }

        // GPS coordinates
        public List<PathCoord> coordsList
        {
            get => _coordsList;
            set => _coordsList = value;
        }

        // total or max length of ride pauses in ticks, otherwise 0 if no pauses
        public long pauseDuration
        {
            get => _pauseDuration;
            set => _pauseDuration = value;
        }

        // stores list of pauses during ride, otherwise empty if no pauses
        public List<item> pausesList
        {
            get => _pausesList;
            set => _pausesList = value;
        }
    }


    /// <summary>
    /// This class stores the list of pauses.
    /// </summary>
    [Serializable()]
    public partial class item
    {
        private ItemKey _key;

        private ItemValue _value;


        public ItemKey key
        {
            get => _key;
            set => _key = value;
        }

        public ItemValue value
        {
            get => _value;
            set => _value = value;
        }
    }


    /// <summary>
    /// Coordinate key where the pause occurred.
    /// </summary>
    [Serializable()]
    public partial class ItemKey
    {
        private int _int;


        public int @int
        {
            get => _int;
            set => _int = value;
        }
    }


    /// <summary>
    /// Duration of the pause in ticks.
    /// </summary>
    [Serializable()]
    public partial class ItemValue
    {
        private long _long;

        
        public long @long
        {
            get => _long;
            set => _long = value;
        }
    }


    /// <summary>
    /// This class stores the GPS path coordinates.
    /// </summary>
    [Serializable()]
    public partial class PathCoord
    {
        private DateTime _timeGathered;

        private coordUnit _coord;

        private double _dWalkDistance;

        private double _dCalories;


        public DateTime timeGathered
        {
            get => _timeGathered;
            set => _timeGathered = value;
        }

        public coordUnit coord
        {
            get => _coord;
            set => _coord = value;
        }

        // notice the name of this node, hence why I suspect that the Pedometer app also used this XML schema
        public double dWalkDistance
        {
            get => _dWalkDistance;
            set => _dWalkDistance = value;
        }

        public double dCalories
        {
            get => _dCalories;
            set => _dCalories = value;
        }
    }


    /// <summary>
    /// This class stores location data and an empty timestamp. A possible future features?
    /// </summary>
    [Serializable()]
    public partial class coordUnit
    {
        private LocationCoord _location;

        private List<int> _timestamp;


        // GPS location type
        public LocationCoord Location
        {
            get => _location;
            set => _location = value;
        }

        public List<int> Timestamp
        {
            get => _timestamp;
            set => _timestamp = value;
        }
    }


    /// <summary>
    /// This class stores the basic GPS location data.
    /// </summary>
    [Serializable()]
    public partial class LocationCoord
    {
        private double _latitude;

        private double _longitude;

        private double _altitude;

        private double _horizontalAccuracy;

        private double _verticalAccuracy;

        private double _speed;

        private double _course;


        public double Latitude
        {
            get => _latitude;
            set => _latitude = value;
        }

        public double Longitude
        {
            get => _longitude;
            set => _longitude = value;
        }

        public double Altitude
        {
            get => _altitude;
            set => _altitude = value;
        }

        public double HorizontalAccuracy
        {
            get => _horizontalAccuracy;
            set => _horizontalAccuracy = value;
        }

        public double VerticalAccuracy
        {
            get => _verticalAccuracy;
            set => _verticalAccuracy = value;
        }

        public double Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public double Course
        {
            get => _course;
            set => _course = value;
        }
    }
}
