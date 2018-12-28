using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;


namespace CycleMasterDataConverter
{
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

        // store the folder path to the DAT files
        private string dataPath;

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
            // save the folder path
            dataPath = folderPath;

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
            string csvPausesPath = exportPath + "\\pausedata.csv";
            int count = FileName.Count;

            // temporary DateTime object for the pause duration ticks
            DateTime temp_pd = new DateTime();

            // Create the serializer
            XmlSerializer serializer = new XmlSerializer(typeof(SerializationStruct));
            SerializationStruct deserializedInstance;

            //initialize metadata CSV file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvMetaPath))
            {
                file.WriteLine("ride_id,title,distance,start_time_utc,start_time_utc_gpx,end_time_utc,end_time_utc_gpx," +
                                "duration_seconds,date_and_utc_time,date_and_utc_time_gpx,total_calories_burned,"+
                                "max_speed,weather,course_joy,extra_notes,pause_duration_seconds");
            }

            //initialize ride data CSV file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvDataPath))
            {
                file.WriteLine("ride_id,track_id,point_id,time_gathered_utc,time_adjusted_utc,time_adjusted_utc_gpx,latitude,longitude,altitude," +
                                "horizontal_accuracy,vertical_accuracy,speed,course,distance,calories_burned");
            }

            //initialize pauses data CSV file
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvPausesPath))
            {
                file.WriteLine("ride_id,track_id,pkey,duration_seconds,cumm_seconds");
            }

            //export metadata, ride data, and pauses data CSV files
            for (int i = 0; i < count; i++)
            {
                // construct ride data file path then decompress and deserialize
                string dataFile = dataPath + "\\" + FileName[i];
                deserializedInstance = new SerializationStruct();
                using (GZipStream instream = new GZipStream(File.OpenRead(dataFile), CompressionMode.Decompress))
                {
                    using (StreamReader streamReader = new StreamReader(instream))
                    {
                        deserializedInstance = serializer.Deserialize(streamReader) as SerializationStruct;
                    }
                }

                Console.WriteLine("exporting ride " + (i + 1) + " of " + count);

                // export pauses data if necessary
                int track_id = 1;
                int seconds = 0;
                List<int> pause_tracks = new List<int>();
                List<int> pause_keys = new List<int>();
                List<int> pause_duration = new List<int>();
                if (deserializedInstance.pausesList.Count > 0)
                {
                    foreach (item t_pause in deserializedInstance.pausesList)
                    {
                        // if no time had elapsed after pausing (often at the end of a ride)...
                        if (t_pause.value[0] > 0)
                        {// ...record the pause location and duration in seconds, along with calculated track ID
                            DateTime t_dur = new DateTime(t_pause.value[0]);
                            track_id++;
                            seconds += t_dur.Hour * 3600 + t_dur.Minute * 60 + t_dur.Second;
                            pause_tracks.Add(track_id);
                            pause_keys.Add(t_pause.key[0] + 1);
                            pause_duration.Add(seconds);
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvPausesPath, true))
                            {
                                file.WriteLine(FileName[i].Substring(0, FileName[i].Length - 4) + "," + track_id + "," + (t_pause.key[0] + 1) + "," + (t_dur.Hour * 3600 + t_dur.Minute * 60 + t_dur.Second) + "," + seconds);
                            }
                        }
                    }
                }


                // export metadata
                temp_pd = new DateTime(deserializedInstance.pauseDuration);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvMetaPath, true))
                {
                    file.WriteLine(FileName[i].Substring(0, FileName[i].Length - 4) + ",\"" + Title[i] + "\"," + Distance[i] + "," +
                                    deserializedInstance.startTime.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss") + "," +
                                    deserializedInstance.startTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ") + "," +
                                    deserializedInstance.endTime.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss") + "," +
                                    deserializedInstance.endTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ") + "," +
                                    (duration[i].Hour * 3600 + duration[i].Minute * 60 + duration[i].Second) + "," +
                                    DateOfRoute[i].ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss") + "," +
                                    DateOfRoute[i].ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ") + "," +
                                    CaloriesBurned[i] + "," + deserializedInstance.dMaxSpeed + "," + Weather[i] + "," +
                                    CourseJoy[i] + ",\"" + ExtraNotes[i] + "\"," +
                                    (temp_pd.Hour * 3600 + temp_pd.Minute * 60 + temp_pd.Second));
                }


                //export ride data
                string t_speed;
                string t_course;
                int point_id = 0;
                track_id = 1;
                seconds = 0;

                foreach (PathCoord t_coord in deserializedInstance.coordsList)
                {
                    t_speed = "";
                    t_course = "";
                    point_id++;

                    if (double.IsNaN(t_coord.coord.Location.Speed))
                        t_speed = "";
                    else
                        t_speed = t_coord.coord.Location.Speed.ToString();

                    if (double.IsNaN(t_coord.coord.Location.Course))
                        t_course = "";
                    else
                        t_course = t_coord.coord.Location.Course.ToString();

                    for (int j = pause_keys.Count - 1; j >= 0; j--)
                    {
                        if (point_id > pause_keys[j])
                        {
                            track_id = pause_tracks[j];
                            seconds = pause_duration[j];
                            break;
                        }
                    }

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvDataPath, true))
                    {
                        file.WriteLine(FileName[i].Substring(0, FileName[i].Length - 4) + "," + track_id + "," + point_id + "," +
                                        t_coord.timeGathered.ToUniversalTime().ToString("yyyy/MM/dd HH:mm:ss") + "," +
                                        t_coord.timeGathered.ToUniversalTime().AddSeconds(seconds).ToString("yyyy/MM/dd HH:mm:ss") + "," +
                                        t_coord.timeGathered.ToUniversalTime().AddSeconds(seconds).ToString("yyyy-MM-ddTHH:mm:ssZ") + "," +
                                        t_coord.coord.Location.Latitude + "," + t_coord.coord.Location.Longitude + "," +
                                        t_coord.coord.Location.Altitude + "," + t_coord.coord.Location.HorizontalAccuracy +
                                        "," + t_coord.coord.Location.VerticalAccuracy + "," + t_speed + "," + t_course +
                                        "," + t_coord.dWalkDistance + "," + t_coord.dCalories);
                    }
                }
            }

            Console.WriteLine("Conversion complete!");
            Console.ReadKey();
        }

        public void exportXML(string exportPath)
        {
            //stub for future implementation
        }
    }
}
