using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Common;
using System.IO;
using System.Windows.Forms;

namespace DistanceCalculator.Clustering
{
    class Calculation
    {
        public static IEnumerable<Place> CalculateDendrogram(List<Place> places, DistanceMetric metric)
        {
            AGNESDendrogram agnesDendrogram = new AGNESDendrogram();
            List<Place> toRet = agnesDendrogram.FitTrnaform(places, metric);

            // Generate visual representation of the dendrogram
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new DendrogramForm(agnesDendrogram.Dendrogram));

            return toRet;
        }
    }

}