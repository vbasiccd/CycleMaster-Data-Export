using System;
using System.Xml;

namespace CycleLibrary
{
    public class GpxFile
    {
        private XmlDocument _gpxDoc = new XmlDocument();
        private XmlElement _root;
        private XmlElement _track;
        private XmlElement _trackSegment;


        public GpxFile()
        {

        }

        public void InitializeDocument(string creatorName = "CycleMaster 3.2")
        {
            _root = _gpxDoc.CreateElement("gpx");
            _root.SetAttribute("version", "1.1");
            _root.SetAttribute("creator", creatorName);
            _root.SetAttribute("xmlns", "http://www.topografix.com/GPX/1/1");
            _root.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            _root.SetAttribute("xsi:schemaLocation", "http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd");

            _gpxDoc.AppendChild(_root);
        }

        public void AddTrack(string trackName, string description = null, string trackType = "cycling")
        {
            if (_track != null)
            {
                FinishTrack();
            }

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

        public void FinishTrack()
        {
            if (_track != null)
            {
                _root.AppendChild(_track);

                _track = null;
            }
        }

        public void AddTrackSegment()
        {
            if (_trackSegment != null)
            {
                FinishTrackSegment();
            }

            _trackSegment = _gpxDoc.CreateElement("trkseg");
        }

        public void FinishTrackSegment()
        {
            if (_trackSegment != null)
            {
                _track.AppendChild(_trackSegment);

                _trackSegment = null;
            }
        }

        public void AddTrackPoint(double latitude, double longitude, double elevation, string gpxTime)
        {
            XmlElement point = _gpxDoc.CreateElement("trkpt");

            point.SetAttribute("lat", latitude.ToString());
            point.SetAttribute("lon", longitude.ToString());

            XmlElement temp = _gpxDoc.CreateElement("ele");
            temp.InnerText = elevation.ToString();
            point.AppendChild(temp);

            temp = _gpxDoc.CreateElement("time");
            temp.InnerText = gpxTime;
            point.AppendChild(temp);

            _trackSegment.AppendChild(point);
        }
    }
}
