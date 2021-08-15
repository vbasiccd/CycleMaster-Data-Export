using CycleLibrary.DataClasses;
using System;
using System.Collections.Generic;
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

        private List<TripDetails> _trips;

        private string _baseFolderPath;
        private string _xmlFolderPath;
        private string _csvFolderPath;
        private string _gpxFolderPath;
        private string _tcxFolderPath;


        public ProcessData(List<TripDetails> trips, bool csvExport, bool gpxExport, bool tcxExport)
        {
            _trips = trips;

            _csvExport = csvExport;
            _gpxExport = gpxExport;
            _tcxExport = tcxExport;
        }

        public void AssignBaseFolderPath(string baseFolderPath)
        {

        }
    }
}
