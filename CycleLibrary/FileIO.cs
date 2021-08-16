using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CycleLibrary
{
    public static class FileIO
    {
        public static void WriteXmlFile(ref XmlDocument xmlDocument, string filePath)
        {
            using (TextWriter fileWriter = new StreamWriter(filePath))
            {
                xmlDocument.Save(fileWriter);
            }
        }
    }
}
