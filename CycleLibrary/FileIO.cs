using CycleLibrary.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace CycleLibrary
{
    public static class FileIO
    {
        /// <summary>
        /// Deserialize the trip details metadata XML file.
        /// </summary>
        /// <param name="filePath">path to the DAT file</param>
        /// <returns>metadata List object</returns>
        public static List<TripDetailsItem> ImportTripMetadataFile(string filePath)
        {
            TripDetails metadata = new TripDetails();

            try
            {
                using (TextReader fileReader = new StreamReader(filePath))
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(TripDetails));
                    metadata = mySerializer.Deserialize(fileReader) as TripDetails;
                }
            }
            catch (InvalidOperationException)
            {
                // NEED SOMETHING; MAYBE A BOOLEAN FLAG
            }

            // NEED A CHECK FIRST BEFORE RETURNING
            return metadata.TripDetailsItem;
        }

        /// <summary>
        /// Deserialize a provided ride data XML file.
        /// </summary>
        /// <param name="filePath">path to the DAT file</param>
        /// <returns>deserialized class object</returns>
        public static SerializationStruct ImportRideDataFile(string filePath)
        {
            SerializationStruct rideData = new SerializationStruct();

            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(SerializationStruct));

                // attempt to decompress the ride data file
                using (GZipStream instream = new GZipStream(File.OpenRead(filePath), CompressionMode.Decompress))
                {
                    // attempt to deserialize the decompressed ride data XML file
                    using (StreamReader streamReader = new StreamReader(instream))
                    {
                        rideData = mySerializer.Deserialize(streamReader) as SerializationStruct;
                    }
                }
            }
            catch (Exception)
            {
                // RETURN TO THIS
            }

            // NEED A CHECK FIRST BEFORE RETURNING
            return rideData;
        }

        /// <summary>
        /// Write an XML document to file.
        /// </summary>
        /// <param name="xmlDocument">XmlDocument object to write</param>
        /// <param name="filePath">destination file path</param>
        public static void WriteXmlFile(ref XmlDocument xmlDocument, string filePath)
        {
            // NEED TO ADD A TRY-CATCH

            using (TextWriter fileWriter = new StreamWriter(filePath))
            {
                xmlDocument.Save(fileWriter);
            }
        }

        /// <summary>
        /// Write a string to a text file.
        /// </summary>
        /// <param name="fileContents">text to write</param>
        /// <param name="filePath">destination file path</param>
        /// <param name="isAppend">True to append existing file; False to overwrite</param>
        public static void WriteTextFile(string fileContents, string filePath, bool isAppend = false)
        {
            // NEED TO ADD A TRY-CATCH

            using (TextWriter file = new StreamWriter(filePath, isAppend))
            {
                file.WriteLine(fileContents);
            }
        }

        /// <summary>
        /// Merge two path segments (directory or file) into one path.
        /// </summary>
        /// <param name="firstPart">first path segment</param>
        /// <param name="secondPart">second path segment</param>
        /// <returns>combined path</returns>
        public static string MergeTwoPathParts(string firstPart, string secondPart)
        {
            return Path.Combine(firstPart, secondPart);
        }

        /// <summary>
        /// Returns the file name without extension.
        /// </summary>
        /// <param name="filePath">path to file</param>
        /// <returns>file name with no extension</returns>
        public static string FileNameOnly(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        /// <summary>
        /// Checks if a file exists. If the optional second path is provided, then the two paths will first be combined.
        /// </summary>
        /// <param name="filePath">file path to check</param>
        /// <param name="filePath2">additional path segment (optional)</param>
        /// <returns>True if file exists; False otherwise</returns>
        public static bool FileExists(string filePath, string filePath2 = null)
        {
            string path = filePath;

            if (filePath2 != null)
            {
                path = Path.Combine(filePath, filePath2);
            }

            return File.Exists(path);
        }

        /// <summary>
        /// Checks if a folder exists.
        /// </summary>
        /// <param name="folderPath">path to folder</param>
        /// <returns>True if folder exists; False otherwise</returns>
        public static bool FolderExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }
    }
}
