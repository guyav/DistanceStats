using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace DistanceCalculator.Common.ExtensionMethods
{
    static class ExtensionMethods
    {
        public static string GetName(this OsmSharp.OsmGeo element)
        {
            if (element.Tags.ContainsKey("name"))
            {
                return element.Tags["name"];
            }
            else if (element.Tags.ContainsKey("name:he"))
            {
                return element.Tags["name:he"];
            }
            else if (element.Tags.ContainsKey("name:en"))
            {
                return element.Tags["name:en"];
            }
            else if (element.Tags.ContainsKey("int_name"))
            {
                return element.Tags["int_name"];
            }
            else
            {
                throw new ArgumentException(string.Format("The element at ({0}) has no valid name.", element.GetCoordinate()));
            }
        }
        public static GeoCoordinate GetCoordinate(this OsmSharp.OsmGeo element)
        {
            return new GeoCoordinate((double)((OsmSharp.Node)element).Latitude, (double)((OsmSharp.Node)element).Longitude);
        }

        public static List<Place> TakeRandom(this List<Place> src, int count, int seed=42)
        {
            if (count >= src.Count)
            {
                return src;
            }

            List<Place> toRet = new List<Place>();
            int[] indices = new int[count];
            Random r = new Random(seed);
            int randomIndex;
            for (int i = 0; i < count; i++)
            {
                do
                {
                    randomIndex = r.Next(0, src.Count);
                }
                while (indices.Contains(randomIndex));
                indices[i] = randomIndex;
                toRet.Add(src[randomIndex]);
            }
            return toRet;
        }
    }
}
