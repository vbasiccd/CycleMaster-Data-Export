using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CycleMasterDataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string mainFolderPath = "C:\\Users\\brett.holland\\Desktop\\cyclemaster data convert";

            //string csvMetaPath = mainFolderPath + "\\metadata.csv";
            //string csvDataPath = mainFolderPath + "\\metadats.csv";
            //string gpxPath =

            MetaData trips = new MetaData();
            trips.LoadTripData(mainFolderPath + "\\data");
            trips.exportCSV(mainFolderPath);

            // Create the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(SerializationStruct));

/*            //test with random data first
            string dataFile = mainFolderPath + "\\test.xml";
            SerializationStruct instance = new SerializationStruct();
            instance.startTime = DateTime.Now;
            instance.endTime = DateTime.Now.AddHours(2.0);
            instance.dMaxSpeed = 50.2;

            LocationCoord lc = new LocationCoord();
            lc.Latitude = 1.5;
            lc.Longitude = 1.5;
            lc.Altitude = 578.2;
            lc.HorizontalAccuracy = 3;
            lc.VerticalAccuracy = 3;
            lc.Speed = 20.1;
            lc.Course = 23.4;
            coordUnit tcu = new coordUnit();
            tcu.Location = lc;
            tcu.Timestamp = new List<int>();
            PathCoord pc = new PathCoord();
            pc.timeGathered = DateTime.Now;
            pc.coord = tcu;
            pc.dWalkDistance = 20;
            pc.dCalories = 200;
            PathCoord pc2 = new PathCoord();
            pc2.timeGathered = DateTime.Now.AddMinutes(2.0);
            pc2.coord = tcu;
            pc2.dWalkDistance = 30;
            pc2.dCalories = 250;

            instance.coordsList.Add(pc);
            instance.coordsList.Add(pc2);
            instance.pauseDuration = 0;
            instance.pausesList = new List<int>();
            
            // Serialize the object to an XML file
            using (StreamWriter streamWriter = File.CreateText(dataFile))
            {
                serializer.Serialize(streamWriter, instance);
            }*/


            //test serialization using uncompressed .dat file (saved as .xml)
            string dataFile = mainFolderPath + "\\140507_050046.gz";

            // Deserialize the object
            SerializationStruct deserializedInstance;
            using (GZipStream instream = new GZipStream(File.OpenRead(dataFile), CompressionMode.Decompress))
            {
                using (StreamReader streamReader = new StreamReader(instream))
                {
                    deserializedInstance = serializer.Deserialize(streamReader) as SerializationStruct;
                }
            }

            Console.WriteLine("Deserialize complete!");
            Console.WriteLine(deserializedInstance.endTime);
            Console.WriteLine(deserializedInstance.coordsList[0].coord.Location.Latitude);
            Console.WriteLine(deserializedInstance.coordsList.Count);
            Console.ReadKey();
        }
    }


    // this class loads and stores the trips.dat file, and holds the data export methods
    public class MetaData
    {
        private List<string> FileName;
        private List<string> Title;
        private List<double> Distance;
        private List<DateTime> duration;
        private List<DateTime> DateOfRoute;
        private List<int> CaloriesBurned;
        private List<int> Weather;
        private List<int> CourseJoy;
        private List<string> ExtraNotes;

        public MetaData()
        {
            FileName = new List<string>();
            Title = new List<string>();
            Distance = new List<double>();
            duration = new List<DateTime>();
            DateOfRoute = new List<DateTime>();
            CaloriesBurned = new List<int>();
            Weather = new List<int>();
            CourseJoy = new List<int>();
            ExtraNotes = new List<string>();
        }

        public void LoadTripData(string folderPath)
        {
            string metadataPath = folderPath + "\\trips.dat";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(metadataPath);
            XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName("TripDetailsItem");

            foreach (XmlNode node in xmlNodes)
            {
                XmlNodeReader xmlNodeReader = new XmlNodeReader(node);
                xmlNodeReader.Read();
                xmlNodeReader.Read();
                FileName.Add(xmlNodeReader.ReadElementContentAsString());
                Title.Add(xmlNodeReader.ReadElementContentAsString());
                Distance.Add(xmlNodeReader.ReadElementContentAsDouble());
                duration.Add(new DateTime(xmlNodeReader.ReadElementContentAsLong()));
                DateOfRoute.Add(xmlNodeReader.ReadElementContentAsDateTime());
                CaloriesBurned.Add(xmlNodeReader.ReadElementContentAsInt());
                Weather.Add(xmlNodeReader.ReadElementContentAsInt());
                CourseJoy.Add(xmlNodeReader.ReadElementContentAsInt());
                ExtraNotes.Add(xmlNodeReader.ReadElementContentAsString());
            }
        }

        public void ReloadTripData(string folderPath)
        {
            FileName = new List<string>();
            Title = new List<string>();
            Distance = new List<double>();
            duration = new List<DateTime>();
            DateOfRoute = new List<DateTime>();
            CaloriesBurned = new List<int>();
            Weather = new List<int>();
            CourseJoy = new List<int>();
            ExtraNotes = new List<string>();
            LoadTripData(folderPath);
        }

        public void exportCSV(string exportPath)
        {
            string csvMetaPath = exportPath + "\\metadata.csv";
            string csvDataPath = exportPath + "\\ridedata.csv";
            int count = FileName.Count;
            //need deserializer

            //initialize metadata CSV file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvMetaPath))
            {
                file.WriteLine("ride_id,title,distance,start_time,end_time,duration_seconds,date_and_utc_time,total_calories_burned,max_speed,weather,course_joy,extra_notes,pause_duration");
            }

            //initialize ride data CSV file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvDataPath))
            {
                file.WriteLine("ride_id,time_gathered,latitude,longitude,altitude,horizontal_accuracy,vertical_accuracy,speed,course,distance,calories_burned");
            }

            //export metadata CSV and ride data CSV
            for (int i = 0; i < count; i++)
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvMetaPath, true))
                {
                    file.WriteLine(FileName[i].Substring(0, FileName[i].Length - 4) + ", \"" + Title[i] + "\" ," + Distance[i] + "," + "placeholderstart" + "," + "placeholderend" + "," + (duration[i].Hour * 3600 + duration[i].Minute * 60 + duration[i].Second) + "," + DateOfRoute[i].ToUniversalTime() + "," + CaloriesBurned[i] + "," + "placeholdermax" + "," + Weather[i] + "," + CourseJoy[i] + ", \"" + ExtraNotes[i] + "\"" + "," + "placeholderpause");
                }
            }

            Console.WriteLine("Conversion complete!");
            Console.ReadKey();
        }

        public void exportGPX(string exportPath)
        {

        }
    }



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

        // placeholder - not sure what this variable was supposed to do
        private int pauseValue;
        public int pauseDuration
        {
            get { return pauseValue; }
            set { pauseValue = value; }
        }

        // placeholder - not sure what this variable was supposed to do
        private List<int> pauseListValue = new List<int>();
        public List<int> pausesList
        {
            get { return pauseListValue; }
            set { pauseListValue = value; }
        }
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
