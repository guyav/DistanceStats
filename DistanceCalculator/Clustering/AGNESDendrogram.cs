using DistanceCalculator.Common;
using System.Collections.Generic;
using System.Linq;

namespace DistanceCalculator.Clustering
{
    internal class AGNESDendrogram
    {
        public Dendrogram Dendrogram { get; private set; }
        private List<Place> placesByRemoteness;

        public AGNESDendrogram()
        {
            this.placesByRemoteness = new List<Place>();
        }

        public List<Place> FitTrnaform(List<Place> places, DistanceMetric metric)
        {
            List<Dendrogram> tempDendrograms = places.Select(place => new LeafNode(place)).ToList().ConvertAll(leaf => (Dendrogram)leaf);
            while (tempDendrograms.Count > 1)
            {
                double minDist = double.MaxValue;
                Dendrogram minDend1 = null, minDend2 = null;
                foreach (Dendrogram dend1 in tempDendrograms)
                {
                    foreach (Dendrogram dend2 in tempDendrograms)
                    {
                        if (dend1 == dend2)
                        {
                            continue;
                        }
                        double tempDist = dend1.DistanceTo(dend2, metric);
                        if (tempDist < minDist)
                        {
                            minDist = tempDist;
                            minDend1 = dend1;
                            minDend2 = dend2;
                        }
                    }
                }

                if (minDend1 is LeafNode)
                {
                    this.placesByRemoteness.Add(minDend1.Places[0]);
                }
                if (minDend2 is LeafNode)
                {
                    this.placesByRemoteness.Add(minDend2.Places[0]);
                }

                Dendrogram combinedDend = new InternalNode(minDend1, minDend2, minDist);
                tempDendrograms.Remove(minDend1);
                tempDendrograms.Remove(minDend2);
                tempDendrograms.Add(combinedDend);
            }
            this.Dendrogram = tempDendrograms[0];
            this.placesByRemoteness.Reverse();
            return this.placesByRemoteness;
        }
    }
}