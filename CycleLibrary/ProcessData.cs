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
                    extract.RideMetadata.StartTimeUtcString = extract.RideMetadata.StartTimeUtc.ToString("yyyy-MM-ddTHH:mm:ssZ");
                    extract.RideMetadata.EndTimeLocal = rideData.endTime;
                    extract.RideMetadata.EndTimeUtc = rideData.endTime.ToUniversalTime();
                    extract.RideMetadata.EndTimeUtcString = extract.RideMetadata.EndTimeUtc.ToString("yyyy-MM-ddTHH:mm:ssZ");

                    DateTime pauseTicks = new DateTime(rideData.pauseDuration);
                    DateTime durationTicks = new DateTime(_trips[i].DurationTicks);
                    extract.RideMetadata.DurationSeconds = durationTicks.Hour * 3600 + durationTicks.Minute * 60 + durationTicks.Second;
                    extract.RideMetadata.PauseSeconds = pauseTicks.Hour * 3600 + pauseTicks.Minute * 60 + pauseTicks.Second;

                    extract.RideMetadata.RideDateTimeLocal = _trips[i].DateOfRoute;
                    extract.RideMetadata.RideDateTimeUtc = _trips[i].DateOfRoute.ToUniversalTime();
                    extract.RideMetadata.RideDateTimeUtcString = extract.RideMetadata.RideDateTimeUtc.ToString("yyyy-MM-ddTHH:mm:ssZ");

                    extract.RideMetadata.TotalCaloriesBurned = _trips[i].CaloriesBurned;
                    extract.RideMetadata.MaxSpeed = rideData.dMaxSpeed;
                    extract.RideMetadata.Weather = _weather[_trips[i].Weather];
                    extract.RideMetadata.CourseJoy = _trips[i].CourseJoy;
                    extract.RideMetadata.ExtraNotes = _trips[i].ExtraNotes;

                    // process pauses
                }
            }
        }
    }
}
