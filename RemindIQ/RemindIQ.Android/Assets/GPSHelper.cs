using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RemindIQ.Droid.Resources
{
    class GPSHelper 
    {
        private static readonly DistanceUnits UNITS = DistanceUnits.Miles;

        private Location location;
        private string locationName;

        public Location Location { get => location; set => location = value; }
        public string LocationName { get => locationName; set => locationName = value; }

        GPSHelper()
        {
            TryToGetCurrentLocation();
            LocationName = null;
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
                }
            }
            catch (Exception)
            {

            }

        }

        public static double GetDistanceBetween (Location location1, Location location2)
        {
            
            double value = LocationExtensions.CalculateDistance(location1, location2, UNITS);
            return value;
        }

        public override string ToString()
        {
            string msg = "";
            if(LocationName != null)
            {
                msg += LocationName;
            }
            else
            {
                msg += "Latitude: " + Location.Latitude + " Longitude: " + Location.Longitude;
            }
            return msg;
        }
    }
}