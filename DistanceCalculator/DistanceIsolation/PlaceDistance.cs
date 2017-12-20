using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Common;

namespace DistanceCalculator.DistanceIsolation
{
    class ClosestPlace:Place
    {
        public double distance;
        public Place closestPlace;

        public ClosestPlace(Place place, Place closestPlace, double distance) : base(place.name, place.type, place.location)
        {
            this.distance = distance;
            this.closestPlace = closestPlace;
        }

        public override string ToString()
        {
            return string.Format("{0}←{1}={2}", this.name, this.closestPlace.name, Math.Round(this.distance / 1000));
        }
    }
}
