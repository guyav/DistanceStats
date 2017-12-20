using DistanceCalculator.Clustering;
using DistanceCalculator.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DistanceCalculator
{
    public class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            List<Place> places = Place.GetPlaces(Path.Combine(string.Format(@"C:\Users\{0}\Desktop\israel-and-palestine-latest.osm.pbf", Environment.UserName)), hebrewOnly: true);
            IEnumerable<Place> clusteringSingle = Clustering.Calculation.CalculateDendrogram(places, DistanceMetric.SingleLink);
            IEnumerable<Place> clusteringComplete = Clustering.Calculation.CalculateDendrogram(places, DistanceMetric.CompleteLink);
            IEnumerable<Place> clusteringAvg = Clustering.Calculation.CalculateDendrogram(places, DistanceMetric.AverageLink);
            IEnumerable<Place> clusteringCentroid = Clustering.Calculation.CalculateDendrogram(places, DistanceMetric.CentroidToCentroid);
            IEnumerable<Place> isolation = DistanceIsolation.Calculation.CaulculateCompleteIsolation(places);
            IEnumerable<Place> citiesIsolation = DistanceIsolation.Calculation.CalculateMajorCitiesIsolation(places, "תל אביב-יפו", "ירושלים", "אילת", "חיפה", "באר שבע");
        }
    }
}