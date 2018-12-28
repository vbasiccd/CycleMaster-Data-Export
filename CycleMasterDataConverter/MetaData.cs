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
    // the trips.dat metadata file is a plain XML file, whereas the DAT files for the
    // recorded trips are gzip compressed serialized XML files
    public class MetaData
    {
        // list variables to store the data in the trips.dat file
        private List<string> FileName;
        private List<string> Title;
        private List<double> Distance;
        private List<DateTime> duration;
        private List<DateTime> DateOfRoute;
        private List<int> CaloriesBurned;
        private List<int> Weather;
        private List<int> CourseJoy;
        private List<string> ExtraNotes;

        // variable for the folder path to the DAT files
        private string dataPath;

        public MetaData()
        {
            // initialize the List variables
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
            // save the base folder path
            dataPath = folderPath;

            // generate path to the trips.dat XML, and then load it
            string metadataPath = folderPath + "\\trips.dat";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(metadataPath);
            // grab ride list via the XML root node in trips.dat
            XmlNodeList xmlNodes = xmlDocument.GetElementsByTagName("TripDetailsItem");

            foreach (XmlNode node in xmlNodes)
            {// loop through the ride list and store the metadata into the variables
                XmlNodeReader xmlNodeReader = new XmlNodeReader(node);
                // for some reason, need to "read" twice to access the data
                xmlNodeReader.Read();
                xmlNodeReader.Read();
                FileName.Add(xmlNodeReader.ReadElementContentAsString());
                Title.Add(xmlNodeReader.ReadElementContentAsString());
                Distance.Add(xmlNodeReader.ReadElementContentAsDouble());
                // the duration is stored as ticks; cast as DateTime
                // for conversion to seconds later
                duration.Add(new DateTime(xmlNodeReader.ReadElementContentAsLong()));
                DateOfRoute.Add(xmlNodeReader.ReadElementContentAsDateTime());
                CaloriesBurned.Add(xmlNodeReader.ReadElementContentAsInt());
                Weather.Add(xmlNodeReader.ReadElementContentAsInt());
                CourseJoy.Add(xmlNodeReader.ReadElementContentAsInt());
                ExtraNotes.Add(xmlNodeReader.ReadElementContentAsString());
            }
        }

        public void ReloadTripData(string folderPath)
        {// reinitialize metadata variables for class reuse, if necessary
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
        {// class to export the metadata and ride data to CSV files
            // generate paths to the three CSV files
            // (these files were designed to be imported into a database)
            // METADATA.CSV - stores the metadata for each ride
            // RIDEDATA.CSV - stores the data for all rides
            // PAUSEDATA.CSV - if a ride was paused, the data for that is stored here
            string csvMetaPath = exportPath + "\\metadata.csv";
            string csvDataPath = exportPath + "\\ridedata.csv";
            string csvPausesPath = exportPath + "\\pausedata.csv";

            // store the total number of rides
            int count = FileName.Count;

            // temporary DateTime object for the pause duration ticks
            DateTime temp_pd = new DateTime();

            // Create the XML serializer for the ride data file
            XmlSerializer serializer = new XmlSerializer(typeof(SerializationStruct));
            SerializationStruct deserializedInstance;

            // initialize metadata CSV file with the header
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvMetaPath))
            {
                file.WriteLine("ride_id,title,distance,start_time_utc,start_time_utc_gpx,end_time_utc,end_time_utc_gpx," +
                                "duration_seconds,date_and_utc_time,date_and_utc_time_gpx,total_calories_burned,"+
                                "max_speed,weather,course_joy,extra_notes,pause_duration_seconds");
            }

            // initialize ride data CSV file with the header
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvDataPath))
            {
                file.WriteLine("ride_id,track_id,point_id,time_gathered_utc,time_adjusted_utc,time_adjusted_utc_gpx,latitude,longitude,altitude," +
                                "horizontal_accuracy,vertical_accuracy,speed,course,distance,calories_burned");
            }

            // initialize pauses data CSV file with the header
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvPausesPath))
            {
                file.WriteLine("ride_id,track_id,pkey,duration_seconds,cumm_seconds");
            }

            // lopp to export metadata, ride data, and pauses data to the CSV files
            for (int i = 0; i < count; i++)
            {// loop through each ride data file
                // construct the ride data file path, then decompress and deserialize
                string dataFile = dataPath + "\\" + FileName[i];
                deserializedInstance = new SerializationStruct();
                using (GZipStream instream = new GZipStream(File.OpenRead(dataFile), CompressionMode.Decompress))
                {// attempt to decompress the ride data file
                    using (StreamReader streamReader = new StreamReader(instream))
                    {// attempt to deserialize the decompressed ride data XML file
                        deserializedInstance = serializer.Deserialize(streamReader) as SerializationStruct;
                    }
                }

                // report progress to the console
                Console.WriteLine("exporting ride " + (i + 1) + " of " + count);

                // export pauses data if necessary
                // if the ride was not paused, then all of the data will
                // have a track_id of 1, otherwise each pause will increment the track value
                // GPX or TCX may need this info to properly acknowledge the pauses
                int track_id = 1;
                int seconds = 0;
                // temporary Lists to store the pause data
                List<int> pause_tracks = new List<int>();
                List<int> pause_keys = new List<int>();
                List<int> pause_duration = new List<int>();
                if (deserializedInstance.pausesList.Count > 0)
                {// if the ride was paused...
                    foreach (item t_pause in deserializedInstance.pausesList)
                    {// ...extract the data
                        // if time had elapsed after pausing...
                        if (t_pause.value[0] > 0)
                        {// ...record the pause location and duration in seconds, along with calculated track ID
                        // otherwise, the pause is not worth extracting if the length is 0, which may occur at the end of a ride
                            DateTime t_dur = new DateTime(t_pause.value[0]);
                            track_id++;
                            // the value for each pause is ONLY the length of THAT pause
                            // to properly adjust the timestamps after each pause (done later),
                            // the cummulative number of seconds must be used
                            seconds += t_dur.Hour * 3600 + t_dur.Minute * 60 + t_dur.Second;
                            pause_tracks.Add(track_id);
                            pause_keys.Add(t_pause.key[0] + 1);
                            pause_duration.Add(seconds);
                            // write the extracted and calculated pause data to the CSV file
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(csvPausesPath, true))
                            {
                                file.WriteLine(FileName[i].Substring(0, FileName[i].Length - 4) + "," + track_id + "," + (t_pause.key[0] + 1) + "," + (t_dur.Hour * 3600 + t_dur.Minute * 60 + t_dur.Second) + "," + seconds);
                            }
                        }
                    }
                }


                // export metadata to the CSV file
                // convert all timestamps from local time to UTC,
                // and include a GPX compatible version
                // convert all duration ticks to seconds
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


                // export ride data to the CSV file
                // create extra variables for data import and conversion,
                // including the track ID as described for the pauses data above
                // the point ID ensures that the GPS coordinates remain in the proper order,
                // useful if using a DBMS to generate GPX or TCX files
                string t_speed;
                string t_course;
                int point_id = 0;
                track_id = 1;
                seconds = 0;

                // loop through every PathCoord node, which stores the GPS data
                // at the start and end of a ride, the speed and course values may be NaN,
                // which may cause DBMS problems or casting erors, so replace these with empty strings
                foreach (PathCoord t_coord in deserializedInstance.coordsList)
                {
                    // initialize variables
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
                    {// loop backwards through the ride pauses
                        // if the current point ID is greater than the pause location key...
                        if (point_id > pause_keys[j])
                        {// ...then use that pause's track ID and cummulative seconds
                            track_id = pause_tracks[j];
                            seconds = pause_duration[j];
                            break;
                        }// this pairs the proper track ID and timestamp adjustment to the GPS coordinate
                    }

                    // write the extracted and calculated ride data to the CSV file,
                    // again converting the timestamps to UTC
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

            // extraction and conversions complete!
            Console.WriteLine("Conversion complete!");
            Console.ReadKey();
        }

        public void exportXML(string exportPath)
        {
            //stub for future implementation
        }
    }
}
