using DistanceCalculator.Common.ExtensionMethods;
using OsmSharp.Streams;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;

namespace DistanceCalculator.Common
{
    internal class Place
    {
        public GeoCoordinate location;
        public string name, type;
        private Dictionary<Place, double> distCache;

        public Place(string name, string type, GeoCoordinate location)
        {
            this.name = name;
            this.type = type;
            this.location = location;
            this.distCache = new Dictionary<Place, double>();
        }

        /// <summary>
        /// The function finds all the nodes with "place" key. It tries to take "name". If it does not exist, it takes "name:he" and then "name:en" and then "int_name"
        /// </summary>
        /// <param name="path">Path of PBF file</param>
        /// <param name="hebrewOnly">Take only places with hebrew names</param>
        /// <returns></returns>
        public static List<Place> GetPlaces(string path, bool hebrewOnly = false)
        {
            using (var fileStream = File.OpenRead(path))
            {
                // create source stream.
                var source = new PBFOsmStreamSource(fileStream);
                List<Place> places = new List<Place>();

                foreach (var element in source)
                {
                    if (element.Type == OsmSharp.OsmGeoType.Node)
                    {
                        if (element.Tags.ContainsKey("place"))
                        {
                            string place = element.Tags["place"];
                            if (place == "city" || place == "village" || place == "town" || place == "locality")
                            {
                                GeoCoordinate coordinate = element.GetCoordinate();
                                string elementName = element.GetName();
                                places.Add(new Place(elementName, place, coordinate));
                            }
                        }
                    }
                }

                if (hebrewOnly)
                {
                    List<Place> hebrewPlaces = new List<Place>();
                    foreach (Place place in places)
                    {
                        if (place.name.IsHebrew())
                        {
                            hebrewPlaces.Add(place);
                        }
                    }
                    places = hebrewPlaces;
                }
                return places;
            }
        }

        public double DistanceTo(Place dst)
        {
            if (this.distCache.ContainsKey(dst))
            {
                return this.distCache[dst];
            }
            else
            {
                double dist = this.location.GetDistanceTo(dst.location);
                this.distCache.Add(dst, dist);
                return dist;
            }
        }

        public DistanceIsolation.ClosestPlace GetClosestPlace(List<Place> placesToCompare)
        {
            Place minDistPlace = null;
            double minDist = double.MaxValue;
            foreach (Place placeToCompare in placesToCompare)
            {
                if (placeToCompare == this)
                {
                    continue;
                }

                double distance = this.DistanceTo(placeToCompare);
                if (distance < minDist)
                {
                    minDist = distance;
                    minDistPlace = placeToCompare;
                }
            }
            return new DistanceIsolation.ClosestPlace(this, minDistPlace, minDist);
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}