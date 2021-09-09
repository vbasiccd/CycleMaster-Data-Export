using System;
using System.Collections.Generic;

namespace CycleLibrary.DataClasses
{
    /// <summary>
    /// Root serialization class.
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("ArrayOfTripDetailsItem")]
    public partial class TripDetails
    {
        private List<TripDetailsItem> _tripDetailsItem;

        [System.Xml.Serialization.XmlElement("TripDetailsItem")]
        public List<TripDetailsItem> TripDetailsItem
        {
            get
            {
                return _tripDetailsItem;
            }
            set
            {
                _tripDetailsItem = value;
            }
        }
    }


    /// <summary>
    /// Class to store the metadata for each trip after deserialization.
    /// </summary>
    [Serializable()]
    public partial class TripDetailsItem
    {
        private string _fileName;

        private string _descr;

        private double _distance;

        private long _durationTicks;

        private DateTime _dateOfRoute;

        private int _caloriesBurned;

        private int _weather;

        private int _courseJoy;

        private string _extraNotes;

        
        public string FileName
        {
            get => _fileName;
            set => _fileName = value;
        }

        public string Descr
        {
            get => _descr;
            set => _descr = value;
        }

        public double Distance
        {
            get => _distance;
            set => _distance = value;
        }

        public long DurationTicks
        {
            get => _durationTicks;
            set => _durationTicks = value;
        }

        public DateTime DateOfRoute
        {
            get => _dateOfRoute;
            set => _dateOfRoute = value;
        }

        public int CaloriesBurned
        {
            get => _caloriesBurned;
            set => _caloriesBurned = value;
        }

        public int Weather
        {
            get => _weather;
            set => _weather = value;
        }

        public int CourseJoy
        {
            get => _courseJoy;
            set => _courseJoy = value;
        }

        public string ExtraNotes
        {
            get => _extraNotes;
            set => _extraNotes = value;
        }
    }


}
