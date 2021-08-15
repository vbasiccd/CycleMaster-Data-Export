using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CycleLibrary
{
    public class TcxFile
    {
        private XmlDocument _tcxDoc = new XmlDocument();
        private XmlElement _root;
        private XmlElement _activity;

        public TcxFile()
        {

        }

        public void PopulateTCX(ref XmlFile rideFile)
        {
            InitializeDocument(activityId: rideFile.RideMetadata.StartTimeUtcString);

            string notesString = $"Title: {rideFile.RideMetadata.Title}; ";
            notesString += $"Notes: {rideFile.RideMetadata.ExtraNotes}; ";
            notesString += $"Course Joy: {rideFile.RideMetadata.CourseJoy}; ";
            notesString += $"Weather: {rideFile.RideMetadata.Weather}";

            XmlElement notes = _tcxDoc.CreateElement("Notes");
            notes.InnerText = notesString;
            _activity.AppendChild(notes);

            AddLap(rideTracks: rideFile.RideTracks, startTime: rideFile.RideMetadata.StartTimeUtcString,
                    totalSeconds: rideFile.RideMetadata.TotalSeconds, distance: rideFile.RideMetadata.Distance,
                    maxSpeed: rideFile.RideMetadata.MaxSpeed, totalCalories: rideFile.RideMetadata.TotalCaloriesBurned);

            FinalizeDocument();
        }

        public void WriteTcxFile(string filePath)
        {
            using (TextWriter fileWriter = new StreamWriter(filePath))
            {
                _tcxDoc.Save(fileWriter);
            }
        }

        public void ResetTcx()
        {
            _activity = null;
            _root = null;

            _tcxDoc = new XmlDocument();
        }


        private void InitializeDocument(string activityId, string activitySport = "Biking")
        {
            _root = _tcxDoc.CreateElement("TrainingCenterDatabase");
            _root.SetAttribute("xsi:schemaLocation", "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2 http://www.garmin.com/xmlschemas/TrainingCenterDatabasev2.xsd");
            _root.SetAttribute("xmlns:ns5", "http://www.garmin.com/xmlschemas/ActivityGoals/v1");
            _root.SetAttribute("xmlns:ns4", "http://www.garmin.com/xmlschemas/ProfileExtension/v1");
            _root.SetAttribute("xmlns:ns3", "http://www.garmin.com/xmlschemas/ActivityExtension/v2");
            _root.SetAttribute("xmlns:ns2", "http://www.garmin.com/xmlschemas/UserProfile/v2");
            _root.SetAttribute("xmlns", "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");
            _root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");

            XmlElement activities = _tcxDoc.CreateElement("Activities");

            _activity = _tcxDoc.CreateElement("Activity");
            _activity.SetAttribute("Sport", activitySport);
            XmlElement activity_id = _tcxDoc.CreateElement("Id");
            activity_id.InnerText = activityId;

            activities.AppendChild(_activity);
            _root.AppendChild(activities);
            _tcxDoc.AppendChild(_root);
        }

        private void FinalizeDocument(string creatorName = "CycleMaster", string langID = "en",
                                        int versionMajor = 3, int versionMinor = 2, int buildMajor = 0, int buildMinor = 0)
        {
            // the "Creator" node at the end of the "Activity" node

            XmlElement creator = _tcxDoc.CreateElement("Creator");
            creator.SetAttribute("xsi:type", "Device_t");

            XmlElement creator_name = _tcxDoc.CreateElement("Name");
            XmlElement creator_version = _tcxDoc.CreateElement("Version");
            XmlElement version_major = _tcxDoc.CreateElement("VersionMajor");
            XmlElement version_minor = _tcxDoc.CreateElement("VersionMinor");
            XmlElement build_major = _tcxDoc.CreateElement("BuildMajor");
            XmlElement build_minor = _tcxDoc.CreateElement("BuildMinor");

            creator_name.InnerText = creatorName;
            version_major.InnerText = versionMajor.ToString();
            version_minor.InnerText = versionMinor.ToString();
            build_major.InnerText = buildMajor.ToString();
            build_minor.InnerText = buildMinor.ToString();

            creator_version.AppendChild(version_major);
            creator_version.AppendChild(version_minor);
            creator_version.AppendChild(build_major);
            creator_version.AppendChild(build_minor);

            creator.AppendChild(creator_name);
            creator.AppendChild(creator_version);

            _activity.AppendChild(creator);


            // the "Author" node at the end of the root node

            XmlElement author = _tcxDoc.CreateElement("Author");
            author.SetAttribute("xsi:type", "Application_t");

            XmlElement build = _tcxDoc.CreateElement("Build");
            build.AppendChild(creator_version);

            XmlElement lang = _tcxDoc.CreateElement("LangID");
            lang.InnerText = langID;

            author.AppendChild(creator_name);
            author.AppendChild(build);
            author.AppendChild(lang);

            _root.AppendChild(author);
        }

        private void AddLap(List<TrackSegment> rideTracks, string startTime, double totalSeconds, double distance, double maxSpeed,
                            int totalCalories, string intensity = "Active", string triggerMethod = "Manual")
        {
            XmlElement lap = _tcxDoc.CreateElement("Lap");
            lap.SetAttribute("StartTime", startTime);

            XmlElement temp = _tcxDoc.CreateElement("TotalTimeSeconds");
            temp.InnerText = totalSeconds.ToString();
            lap.AppendChild(temp);

            temp = _tcxDoc.CreateElement("DistanceMeters");
            temp.InnerText = distance.ToString();
            lap.AppendChild(temp);

            temp = _tcxDoc.CreateElement("MaximumSpeed");
            temp.InnerText = maxSpeed.ToString();
            lap.AppendChild(temp);

            temp = _tcxDoc.CreateElement("Calories");
            temp.InnerText = totalCalories.ToString();
            lap.AppendChild(temp);

            temp = _tcxDoc.CreateElement("Intensity");
            temp.InnerText = intensity;
            lap.AppendChild(temp);

            temp = _tcxDoc.CreateElement("TriggerMethod");
            temp.InnerText = triggerMethod;
            lap.AppendChild(temp);


            XmlElement track = _tcxDoc.CreateElement("Track");
            foreach(TrackSegment segment in rideTracks)
            {
                // We could split each segment into separate track nodes,
                // yet prior testing revealed that this was not fully compatible in Garmin Connect.
                // So, combine all track points into a single Track node.

                foreach(TrackPoint point in segment.SegmentPoints)
                {
                    AddTrackPoint(ref track, point);
                }
            }


            lap.AppendChild(track);
            _activity.AppendChild(lap);
        }

        private void AddTrackPoint(ref XmlElement track, TrackPoint trackPoint)
        {
            XmlElement point = _tcxDoc.CreateElement("Trackpoint");

            XmlElement time = _tcxDoc.CreateElement("Time");
            time.InnerText = trackPoint.PointTimeUtcString;
            point.AppendChild(time);

            XmlElement position = _tcxDoc.CreateElement("Position");
            XmlElement latitude = _tcxDoc.CreateElement("LatitudeDegrees");
            latitude.InnerText = trackPoint.Latitude.ToString();
            position.AppendChild(latitude);
            XmlElement longitude = _tcxDoc.CreateElement("LongitudeDegrees");
            longitude.InnerText = trackPoint.Longitude.ToString();
            position.AppendChild(longitude);
            point.AppendChild(position);

            XmlElement altitude = _tcxDoc.CreateElement("AltitudeMeters");
            altitude.InnerText = trackPoint.Altitude.ToString();
            point.AppendChild(altitude);

            XmlElement distance = _tcxDoc.CreateElement("DistanceMeters");
            distance.InnerText = trackPoint.Distance.ToString();
            point.AppendChild(distance);

            XmlElement extension = _tcxDoc.CreateElement("Extensions");
            XmlElement tpx = _tcxDoc.CreateElement("ns3:TPX");
            XmlElement speed = _tcxDoc.CreateElement("ns3:Speed");
            speed.InnerText = trackPoint.Speed.ToString();
            tpx.AppendChild(speed);
            extension.AppendChild(tpx);
            point.AppendChild(extension);

            track.AppendChild(point);
        }
    }
}
