using DistanceCalculator.Common;
using System;
using System.Collections.Generic;
using System.Device.Location;

namespace DistanceCalculator.Clustering
{
    internal class LeafNode : Dendrogram
    {
        public Place Place { get; private set; }

        public override List<Place> Places
        {
            get
            {
                if (this.places == null)
                {
                    this.places = new List<Place>() { this.Place };
                }
                return this.places;
            }
        }

        public override GeoCoordinate Centroid
        {
            get
            {
                return Place.location;
            }
        }

        public override string Name
        {
            get
            {
                return this.Place.name;
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
                Place srcPlace = this.Place;
                foreach (Place dstPlace in dst.Places)
                {
                    double distance = srcPlace.DistanceTo(dstPlace);
                    if (distance < minDist)
                    {
                        minDist = distance;
                    }
                }

                toRet = minDist;
            }
            else if (metric == DistanceMetric.CompleteLink)
            {
                double maxDist = double.MinValue;
                Place srcPlace = this.Place;
                foreach (Place dstPlace in dst.Places)
                {
                    double distance = srcPlace.DistanceTo(dstPlace);
                    if (distance > maxDist)
                    {
                        maxDist = distance;
                    }
                }
                toRet = maxDist;
            }
            else if (metric == DistanceMetric.AverageLink)
            {
                double distsSum = 0;
                Place srcPlace = this.Place;
                foreach (Place dstPlace in dst.Places)
                {
                    distsSum += srcPlace.DistanceTo(dstPlace);
                }
                toRet = distsSum / dst.Places.Count;
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

        public LeafNode(Place place)
        {
            this.Place = place;
        }
    }
}