using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace RemindIQ.Services
{
    class LocationHelper
    {
        private static readonly DistanceUnits UNITS = DistanceUnits.Miles;

        private Location location;
        private string locationName;

        public Location Location { get => location; set => location = value; }
        public string LocationName { get => locationName; set => locationName = value; }

        LocationHelper()
        {
            TryToGetCurrentLocation();
            LocationName = null;
        }

        LocationHelper(string loc)
        {
            LocationName = loc;
            TryToGetRemoteLocation(loc);
        }

        private async void GetCoordsByAddress(string address)
        {
            try
            {

                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    location.Longitude.ToString();
                    location.Latitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine("Error: " + fnsEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private async void TryToGetRemoteLocation(string loc)
        {
            try
            {
                var request = await Geocoding.GetLocationsAsync(loc);

                Location = request?.FirstOrDefault();
                if (Location == null)
                {
                    Console.WriteLine("invalid location used, using device location.");
                    TryToGetCurrentLocation();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine("Error: " + fnsEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private async void TryToGetCurrentLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                Location locationTemp = await Geolocation.GetLastKnownLocationAsync();

                if (locationTemp != null)
                {
                    Location = locationTemp;
                    LocationName = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        public static double GetDistanceBetween(Location location1, Location location2)
        {

            double value = LocationExtensions.CalculateDistance(location1, location2, UNITS);
            return value;
        }

        public override string ToString()
        {
            string msg = "";
            if (LocationName != null)
            {
                msg += LocationName;
            }
            else if (Location != null)
            {
                msg += "Latitude: " + Location.Latitude + " Longitude: " + Location.Longitude;
            }
            else
            {
                msg += "Error, no location data to return.";
            }
            return msg;
        }
    }
}
