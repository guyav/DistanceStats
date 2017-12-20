using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Common;
using System.Device.Location;

namespace DistanceCalculator.Clustering
{
    abstract class Dendrogram
    {
        public abstract List<Place> Places { get; }
        public abstract double DistanceTo(Dendrogram dst, DistanceMetric metric);
        public abstract GeoCoordinate Centroid { get; }
        public abstract string Name { get; }
        protected List<Place> places;
        protected Dictionary<Dendrogram, double> distCache;

        public Dendrogram()
        {
            this.distCache = new Dictionary<Dendrogram, double>();
        }
    }
}