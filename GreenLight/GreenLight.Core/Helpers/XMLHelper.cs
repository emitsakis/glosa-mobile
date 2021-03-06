﻿/*
GLOSA Mobile. Green Light Optimal Speed Adviosry Mobile Application

Copyright © 2017 Eastpoint Software Limited

Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
and associated documentation files (the "Software"), to deal in the Software without restriction, 
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR 
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

 */

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

using GreenLight.Core.Models;

namespace GreenLight.Core.Helpers
{
    public static class XMLHelper
    {
        public static MapData LoadMAPDataFromFile(string filename)
        {
            MapData data = null;

            //string filename = $"GreenLight.Core.Test.{file}";
            var assembly = typeof(XMLHelper).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream(filename);

            if (stream != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
                Envelope envelope = serializer.Deserialize(stream) as Envelope;
                
                data = envelope.Body.MapData; 
            }
            else
            {
                throw new FileNotFoundException("Could not find file", filename);
            }

            return data;
        }

        public static IList<MapData> LoadMAPData()
        {
            IList<MapData> data = new List<MapData>();

            var assembly = typeof(XMLHelper).GetTypeInfo().Assembly;

            var resourceNames = assembly.GetManifestResourceNames();
            if (resourceNames.Length == 0)
            {
                return data;
            }

            var MAPDataFiles = resourceNames.Where(name => name.Contains(".MAP-"));
            foreach (var file in MAPDataFiles)
            {
                var mapData = XMLHelper.LoadMAPDataFromFile(file);
                data.Add(mapData);
            }
            return data;
        }

        public static SPAT LoadSPATDataForIntersection(string intersectionId)
        {
            SPAT data = null;

            string file = $"GreenLight.Core.Test.SPAT-{intersectionId}-WiFi.xml";
            var assembly = typeof(XMLHelper).GetTypeInfo().Assembly;

            Stream stream = assembly.GetManifestResourceStream(file);

            if (stream != null)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SPAT));
                    data = serializer.Deserialize(stream) as SPAT;
                }
                catch
                {

                }
            }
            else
            {
                throw new FileNotFoundException("Could not find file", file);
            }

            return data;
        }
    }
}
