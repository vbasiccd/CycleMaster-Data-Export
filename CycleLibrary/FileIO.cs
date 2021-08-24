using CycleLibrary.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CycleLibrary
{
    public static class FileIO
    {
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

        public static void WriteXmlFile(ref XmlDocument xmlDocument, string filePath)
        {
            // NEED TO ADD A TRY-CATCH

            using (TextWriter fileWriter = new StreamWriter(filePath))
            {
                xmlDocument.Save(fileWriter);
            }
        }

        public static void WriteTextFile(string fileContents, string filePath, bool isAppend = false)
        {
            // NEED TO ADD A TRY-CATCH

            using (TextWriter file = new StreamWriter(filePath, isAppend))
            {
                file.WriteLine(fileContents);
            }
        }

        public static string MergeTwoPathParts(string firstPart, string secondPart)
        {
            return Path.Combine(firstPart, secondPart);
        }

        public static string FileNameOnly(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public static bool FileExists(string filePath, string filePath2 = null)
        {
            string path = filePath;

            if (filePath2 != null)
            {
                path = Path.Combine(filePath, filePath2);
            }

            return File.Exists(path);
        }

        public static bool FolderExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }
    }
}
