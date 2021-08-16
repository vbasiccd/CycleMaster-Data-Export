using System.Collections.Generic;
using System.Xml;

namespace CycleLibrary
{
    public class GpxFile
    {
        private XmlDocument _gpxDoc = new XmlDocument();
        private XmlElement _root;
        private XmlElement _track;
        private XmlElement _trackSegment;

        private string _creatorName;


        public GpxFile(string creatorName = "CycleMaster 3.2")
        {
            _creatorName = creatorName;
        }

        public void PopulateGPX(ref XmlFile rideFile)
        {
            InitializeDocument(rideFile.RideMetadata.StartTimeUtcString);

            string descString = $"Title: {rideFile.RideMetadata.Title}; ";
            descString += $"Notes: {rideFile.RideMetadata.ExtraNotes}; ";
            descString += $"Course Joy: {rideFile.RideMetadata.CourseJoy}; ";
            descString += $"Weather: {rideFile.RideMetadata.Weather}";

            AddTrack(trackName: rideFile.RideMetadata.Title, description: descString);

            AddTrackSegments(rideFile.RideTracks);

            _root.AppendChild(_track);
        }

        public void WriteGpxFile(string filePath)
        {
            FileIO.WriteXmlFile(ref _gpxDoc, filePath);
        }

        public void ResetGpx()
        {
            _trackSegment = null;
            _track = null;
            _root = null;

            _gpxDoc = new XmlDocument();
        }

        private void InitializeDocument(string startTime)
        {
            _root = _gpxDoc.CreateElement("gpx");
            _root.SetAttribute("version", "1.1");
            _root.SetAttribute("creator", _creatorName);
            _root.SetAttribute("xmlns", "http://www.topografix.com/GPX/1/1");
            _root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            _root.SetAttribute("xsi:schemaLocation", "http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd");

            XmlElement metadata = _gpxDoc.CreateElement("metadata");
            XmlElement time = _gpxDoc.CreateElement("time");
            time.InnerText = startTime;
            metadata.AppendChild(time);
            _root.AppendChild(metadata);

            _gpxDoc.AppendChild(_root);
        }

        private void AddTrack(string trackName, string description = null, string trackType = "cycling")
        {
            _track = _gpxDoc.CreateElement("trk");

            XmlElement temp = _gpxDoc.CreateElement("name");
            temp.InnerText = trackName;
            _track.AppendChild(temp);

            if (description != null)
            {
                temp = _gpxDoc.CreateElement("desc");
                temp.InnerText = description;
                _track.AppendChild(temp);
            }

            temp = _gpxDoc.CreateElement("type");
            temp.InnerText = trackType;
            _track.AppendChild(temp);
        }

        private void AddTrackSegments(List<TrackSegment> rideTracks)
        {
            foreach(TrackSegment segment in rideTracks)
            {
                _trackSegment = _gpxDoc.CreateElement("trkseg");

                foreach (TrackPoint point in segment.SegmentPoints)
                {
                    AddTrackPoint(point);
                }

                _track.AppendChild(_trackSegment);

                _trackSegment = null;
            }
        }

        private void AddTrackPoint(TrackPoint trackPoint)
        {
            XmlElement point = _gpxDoc.CreateElement("trkpt");

            point.SetAttribute("lat", trackPoint.Latitude.ToString());
            point.SetAttribute("lon", trackPoint.Longitude.ToString());

            XmlElement temp = _gpxDoc.CreateElement("ele");
            temp.InnerText = trackPoint.Altitude.ToString();
            point.AppendChild(temp);

            temp = _gpxDoc.CreateElement("time");
            temp.InnerText = trackPoint.PointTimeUtcString;
            point.AppendChild(temp);

            temp = _gpxDoc.CreateElement("hdop");
            temp.InnerText = trackPoint.HorizontalAccuracy.ToString();
            point.AppendChild(temp);

            temp = _gpxDoc.CreateElement("vdop");
            temp.InnerText = trackPoint.VerticalAccuracy.ToString();
            point.AppendChild(temp);

            temp = _gpxDoc.CreateElement("sym");
            temp.InnerText = "Waypoint";
            point.AppendChild(temp);

            _trackSegment.AppendChild(point);
        }
    }
}
