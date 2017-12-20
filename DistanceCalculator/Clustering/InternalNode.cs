using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Common;
using DistanceCalculator.Common.ExtensionMethods;
using System.Device.Location;

namespace DistanceCalculator.Clustering
{
    class InternalNode : Dendrogram
    {
        public Dendrogram Child1 { get; private set; }
        public Dendrogram Child2 { get; private set; }
        public double Distance { get; private set; }

        public override string Name
        {
            get
            {
                

                IEnumerable<Place> citiesInNode;
                if (this.Places.Count <= 5)
                {
                    citiesInNode = this.Places;
                }
                else
                {
                    citiesInNode = this.Places.Where(place => place.type == "city");

                    // if no cities are found in the node, take 5 regular places from the dendrogram
                    if (citiesInNode.Count() == 0)
                    {
                        citiesInNode = this.Places.TakeRandom(5);
                    }
                }

                return string.Join(", ", citiesInNode);
            }
        }
        public override List<Place> Places
        {
            get
            {
                if (this.places == null)
                {
                    this.places = Child1.Places.Concat(Child2.Places).ToList();
                }
                return this.places;
            }
        }


        public override double DistanceTo(Dendrogram dst, DistanceMetric metric)
        {
            // check if the requested calculation was computed previously
            if (distCache.ContainsKey(dst))
            {
                return distCache[dst];
            }

            double toRet;

            if (metric == DistanceMetric.SingleLink)
            {
                double minDist = double.MaxValue;
                foreach (Place srcPlace in this.Places)
                {
                    foreach (Place dstPlace in dst.Places)
                    {
                        double distance = srcPlace.DistanceTo(dstPlace);
                        if (distance < minDist)
                        {
                            minDist = distance;
                        }
                    }
                }
                toRet = minDist;
            }
            else if (metric == DistanceMetric.CompleteLink)
            {
                double maxDist = double.MinValue;
                foreach (Place srcPlace in this.Places)
                {
                    foreach (Place dstPlace in dst.Places)
                    {
                        double distance = srcPlace.DistanceTo(dstPlace);
                        if (distance > maxDist)
                        {
                            maxDist = distance;
                        }
                    }
                }
                toRet = maxDist;
            }
            else if (metric == DistanceMetric.AverageLink)
            {
                double distsSum = 0;
                foreach (Place srcPlace in this.Places)
                {
                    foreach (Place dstPlace in dst.Places)
                    {
                        distsSum += srcPlace.DistanceTo(dstPlace);
                    }
                }
                toRet = distsSum / (this.Places.Count * dst.Places.Count);
            }
            else if (metric == DistanceMetric.CentroidToCentroid)
            {
                toRet = this.Centroid.GetDistanceTo(dst.Centroid);
            }
            else
            {
                throw new NotImplementedException();
            }

            distCache.Add(dst, toRet);
            return toRet;
        }

        public override GeoCoordinate Centroid
        {
            get
            {
                double sumLatitude = 0, sumLongtitude = 0;
                foreach (Place place in this.Places)
                {
                    sumLatitude += place.location.Latitude;
                    sumLongtitude += place.location.Longitude;
                }
                int placesCount = this.Places.Count();
                return new GeoCoordinate(sumLatitude / placesCount, sumLongtitude / placesCount);
            }
        }


        public InternalNode(Dendrogram child1, Dendrogram child2, double distance)
        {
            this.Child1 = child1;
            this.Child2 = child2;
            this.Distance = distance;
        }
    }
}
