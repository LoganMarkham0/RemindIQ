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
        private readonly DistanceUnits UNITS = DistanceUnits.Miles;
        private Location location;
        private double latitude;
        private double longitude;
        private string locationName;

        GPSHelper()
        {
            this.location = new Location();
            this.latitude = location.Latitude;
            this.longitude = location.Longitude;
            this.locationName = null;
        }

        public double GetDistanceBetween (Location location1, Location location2)
        {
            
            double value = LocationExtensions.CalculateDistance(location1, location2, UNITS);
            return value;
        }

        public override string ToString()
        {
            string msg = "";
            if(locationName != null)
            {
                msg += locationName;
            }
            else
            {
                msg += "Latitude: " + this.latitude + " Longitude: " + this.longitude;
            }
            return msg;
        }
    }
}