/*
 * NOTE:    I still have A LOT of cleanup to do in this class file!  For now, the root data path
 *          is hard-coded before the MetaData object is created and the CSV export begins.
 *          My final goal is to add a simple GUI for ease-of-use.  Before that, I want to
 *          complete the XML export class.
*/

using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


namespace CycleMasterDataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string mainFolderPath = "C:\\Users\\vbasi\\Documents\\Visual Studio 2017\\Projects\\CycleMaster Data Converter";

            //string csvMetaPath = mainFolderPath + "\\metadata.csv";
            //string csvDataPath = mainFolderPath + "\\metadats.csv";
            //string gpxPath =

            MetaData trips = new MetaData();
            trips.LoadTripData(mainFolderPath + "\\data");
            trips.exportCSV(mainFolderPath + "\\Exports\\CSV");
    
            // Create the serializer
            //XmlSerializer serializer = new XmlSerializer(typeof(SerializationStruct));

/*            //test with random data first
            string dataFile = mainFolderPath + "\\test.xml";
            SerializationStruct instance = new SerializationStruct();
            instance.startTime = DateTime.Now;
            instance.endTime = DateTime.Now.AddHours(2.0);
            instance.dMaxSpeed = 50.2;

            LocationCoord lc = new LocationCoord();
            lc.Latitude = 1.5;
            lc.Longitude = 1.5;
            lc.Altitude = 578.2;
            lc.HorizontalAccuracy = 3;
            lc.VerticalAccuracy = 3;
            lc.Speed = 20.1;
            lc.Course = 23.4;
            coordUnit tcu = new coordUnit();
            tcu.Location = lc;
            tcu.Timestamp = new List<int>();
            PathCoord pc = new PathCoord();
            pc.timeGathered = DateTime.Now;
            pc.coord = tcu;
            pc.dWalkDistance = 20;
            pc.dCalories = 200;
            PathCoord pc2 = new PathCoord();
            pc2.timeGathered = DateTime.Now.AddMinutes(2.0);
            pc2.coord = tcu;
            pc2.dWalkDistance = 30;
            pc2.dCalories = 250;

            instance.coordsList.Add(pc);
            instance.coordsList.Add(pc2);
            instance.pauseDuration = 0;
            instance.pausesList = new List<item>();
            item pause1 = new item();
            pause1.key = new List<int>();
            pause1.key.Add(1);
            pause1.value = new List<long>();
            pause1.value.Add(2);
            item pause2 = new item();
            pause2.key = new List<int>();
            pause2.key.Add(3);
            pause2.value = new List<long>();
            pause2.value.Add(4);
            instance.pausesList.Add(pause1);
            instance.pausesList.Add(pause2);
            
            // Serialize the object to an XML file
            using (StreamWriter streamWriter = File.CreateText(dataFile))
            {
                serializer.Serialize(streamWriter, instance);
            }*/

/*
            //test serialization using .dat file
            string dataFile = mainFolderPath + "\\data\\140720_080808.dat";

            //// Deserialize the object
            SerializationStruct deserializedInstance;
            using (GZipStream instream = new GZipStream(File.OpenRead(dataFile), CompressionMode.Decompress))
            {
                using (StreamReader streamReader = new StreamReader(instream))
                {
                    deserializedInstance = serializer.Deserialize(streamReader) as SerializationStruct;
                }
            }

            Console.WriteLine("Deserialize complete!");
            Console.WriteLine(deserializedInstance.endTime);
            Console.WriteLine(deserializedInstance.coordsList[0].coord.Location.Latitude);
            Console.WriteLine(deserializedInstance.coordsList.Count);
            Console.WriteLine(deserializedInstance.pauseDuration);
            Console.WriteLine(deserializedInstance.pausesList.Count);
            Console.ReadKey();*/
        }
    }


        
}
