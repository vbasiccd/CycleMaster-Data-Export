using CycleLibrary.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CycleLibrary
{
    public class ProcessData
    {
        private Dictionary<int, string> _weather = new Dictionary<int, string>()
        {
            { 0,"Sunny" },
            { 1,"Little Cloudy" },
            { 2,"Cloudy" },
            { 3,"Rainy" },
            { 4,"Snowy" },
            { 5,"Stormy" }
        };

        private bool _csvExport = false;
        private bool _gpxExport = false;
        private bool _tcxExport = false;

        private List<TripDetailsItem> _trips;

        private string _rideDataFolder;
        private string _baseOutputFolder;
        private string _xmlOutputFolder;
        private string _csvOutputFolder;
        private string _gpxOutputFolder;
        private string _tcxOutputFolder;


        public ProcessData(List<TripDetailsItem> trips, bool csvExport, bool gpxExport, bool tcxExport)
        {
            _trips = trips;

            _csvExport = csvExport;
            _gpxExport = gpxExport;
            _tcxExport = tcxExport;
        }

        public void AssignFolderPaths(string rideDataFolderPath, string baseOutputFolderPath)
        {
            _rideDataFolder = rideDataFolderPath;

            _baseOutputFolder = baseOutputFolderPath;

            _xmlOutputFolder = FileIO.MergeTwoPathParts(baseOutputFolderPath, "XML");

            if (_csvExport == true)
            {
                _csvOutputFolder = FileIO.MergeTwoPathParts(baseOutputFolderPath, "CSV");
            }

            if (_gpxExport == true)
            {
                _gpxOutputFolder = FileIO.MergeTwoPathParts(baseOutputFolderPath, "GPX");
            }

            if (_tcxExport == true)
            {
                _tcxOutputFolder = FileIO.MergeTwoPathParts(baseOutputFolderPath, "TCX");
            }
        }

        public void ExtractData()
        {
            if (_rideDataFolder != null && _baseOutputFolder != null)
            {
                for (int i = 0; i < _trips.Count; i++)
                {
                    // USE THE FOR LOOP VARIABLE TO RETURN PROGRESS TO THE GUI
                    // VIA AN EVENT HANDLER

                    // Create new XML file object;
                    XmlFile extract = new XmlFile();
                    extract.RideMetadata = new Metadata();

                    // Import the ride data of the current trip.
                    string filePath = FileIO.MergeTwoPathParts(_rideDataFolder, _trips[i].FileName);
                    SerializationStruct rideData = FileIO.ImportRideDataFile(filePath);

                    // Populate the XML metadata.
                    extract.RideMetadata.RideID = FileIO.FileNameOnly(filePath);
                    extract.RideMetadata.Title = _trips[i].Descr;
                    extract.RideMetadata.Distance = _trips[i].Distance;

                    extract.RideMetadata.StartTimeLocal = rideData.startTime;
                    extract.RideMetadata.StartTimeUtc = rideData.startTime.ToUniversalTime();
                    extract.RideMetadata.StartTimeUtcString = TimeToXmlString(extract.RideMetadata.StartTimeUtc);
                    extract.RideMetadata.EndTimeLocal = rideData.endTime;
                    extract.RideMetadata.EndTimeUtc = rideData.endTime.ToUniversalTime();
                    extract.RideMetadata.EndTimeUtcString = TimeToXmlString(extract.RideMetadata.EndTimeUtc);

                    extract.RideMetadata.DurationSeconds = TicksToSeconds(_trips[i].DurationTicks);
                    extract.RideMetadata.PauseSeconds = TicksToSeconds(rideData.pauseDuration);

                    extract.RideMetadata.RideDateTimeLocal = _trips[i].DateOfRoute;
                    extract.RideMetadata.RideDateTimeUtc = _trips[i].DateOfRoute.ToUniversalTime();
                    extract.RideMetadata.RideDateTimeUtcString = TimeToXmlString(extract.RideMetadata.RideDateTimeUtc);

                    extract.RideMetadata.TotalCaloriesBurned = _trips[i].CaloriesBurned;
                    extract.RideMetadata.MaxSpeed = rideData.dMaxSpeed;
                    extract.RideMetadata.Weather = _weather[_trips[i].Weather];
                    extract.RideMetadata.CourseJoy = _trips[i].CourseJoy;
                    extract.RideMetadata.ExtraNotes = _trips[i].ExtraNotes;

                    // Prepare the pause data, if needed.
                    int pauseSeconds = 0;
                    List<int> pauseKeys = new List<int>();
                    List<int> pauseDuration = new List<int>();

                    // if the ride was paused at least once...
                    if (rideData.pausesList.Count > 0)
                    {
                        // ...then for each pause...
                        foreach (item ridePause in rideData.pausesList)
                        {
                            // ...if the length of the pause was greater than zero...
                            if (ridePause.value.@long > 0)
                            {
                                // ...then convert the pause ticks to seconds
                                pauseSeconds += TicksToSeconds(ridePause.value.@long);

                                // add the pause key and seconds to the Lists
                                pauseKeys.Add(ridePause.key.@int);
                                pauseDuration.Add(pauseSeconds);

                                // add a new track to the XmlFile object
                                extract.RideTracks.Add(new TrackSegment());
                            }
                        }
                    }
                    // ...otherwise there were no pauses...
                    else
                    {
                        // ...so just add a single track to the XmlFile object
                        extract.RideTracks.Add(new TrackSegment());
                    }

                    // Populate the ride data.
                    for(int c = 0; c < rideData.coordsList.Count; c++)
                    {
                        PathCoord coord = rideData.coordsList[c];

                        DateTime coordTime = coord.timeGathered;
                        int secondsToAdd = 0;

                        int trackId = 0;

                        for (int j = pauseKeys.Count - 1; j >= 0; j--)
                        {
                            if (c > pauseKeys[j])
                            {
                                trackId = j;
                                secondsToAdd = pauseDuration[j];
                                break;
                            }
                        }

                        coordTime = coordTime.AddSeconds(secondsToAdd);

                        TrackPoint coordPoint = new TrackPoint();
                        coordPoint.PointTimeLocal = coordTime;
                        coordPoint.PointTimeUtc = coordTime.ToUniversalTime();
                        coordPoint.PointTimeUtcString = TimeToXmlString(coordPoint.PointTimeUtc);
                        coordPoint.Latitude = coord.coord.Location.Latitude;
                        coordPoint.Longitude = coord.coord.Location.Longitude;
                        coordPoint.Altitude = coord.coord.Location.Altitude;
                        coordPoint.HorizontalAccuracy = coord.coord.Location.HorizontalAccuracy;
                        coordPoint.VerticalAccuracy = coord.coord.Location.VerticalAccuracy;
                        coordPoint.Speed = coord.coord.Location.Speed;
                        coordPoint.Course = coord.coord.Location.Course;
                        coordPoint.Distance = coord.dWalkDistance;
                        coordPoint.CaloriesBurned = coord.dCalories;

                        extract.RideTracks[trackId].SegmentPoints.Add(coordPoint);
                    }
                }
            }
        }

        private int TicksToSeconds(long ticks)
        {
            DateTime timeTicks = new DateTime(ticks);

            return timeTicks.Hour * 3600 + timeTicks.Minute * 60 + timeTicks.Second;
        }

        private string TimeToXmlString(DateTime UtcTime)
        {
            return UtcTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
