using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using DistanceCalculator.Common;

namespace DistanceCalculator.Clustering
{
    enum DistanceMetric
    {
        CompleteLink,
        SingleLink,
        AverageLink,
        CentroidToCentroid
    }
}