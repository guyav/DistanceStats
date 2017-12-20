using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistanceCalculator.Common;

namespace DistanceCalculator.DistanceIsolation
{
    class Calculation
    {
        public static List<ClosestPlace> CaulculateCompleteIsolation(List<Place> places)
        {
            List<ClosestPlace> closestPlaces = new List<ClosestPlace>();
            foreach (Place place in places)
            {
                closestPlaces.Add(place.GetClosestPlace(places));
            }

            closestPlaces = closestPlaces.OrderByDescending(x => x.distance).ToList();
            return closestPlaces;
        }

        public static List<ClosestPlace> CalculateMajorCitiesIsolation(List<Place> places, params string[] requestedCities)
        {
            // Finding the city names as string in the "places" list
            List<Place> cities = new List<Place>();
            foreach (string city in requestedCities)
            {
                cities.Add(places.First(place => place.name == city));
            }

            List<ClosestPlace> closestPlaces = new List<ClosestPlace>();
            foreach (Place place in places)
            {
                // if the place to check is one of the "anchor cities", skip
                if (cities.Contains(place))
                {
                    closestPlaces.Add(new ClosestPlace(place, place, 0));
                    continue;
                }

                closestPlaces.Add(place.GetClosestPlace(cities));
            }

            closestPlaces = closestPlaces.OrderByDescending(x => x.distance).ToList();
            return closestPlaces;
        }
    }
}
