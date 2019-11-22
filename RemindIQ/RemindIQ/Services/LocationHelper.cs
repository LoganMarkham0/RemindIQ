using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using RemindIQ.Models;

namespace RemindIQ.Services
{
    public class LocationHelper
    {
        private static readonly DistanceUnits UNITS = DistanceUnits.Miles;
        
        Thread Thread;
        int time = 10000;
        int status;
        List<Reminder> fromDatabase;
        
        public LocationHelper()
        {
            Thread = new Thread(UpdateDistance);
        }

        public async Task<Location> GetRemoteLocation(string address)
        {
            try
            {
                var request = await Geocoding.GetLocationsAsync(address);
                Location temp = request?.FirstOrDefault();

                if (temp != null)
                {
                    return temp;
                }
                throw new Exception("Could not resolve address.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                new GeolocationRequest(GeolocationAccuracy.Medium);
                Location temp = await Geolocation.GetLastKnownLocationAsync();

                if (temp != null)
                {
                    return temp;
                }
                throw new Exception("Could not get current location.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<Location> CurrentLocation()
        {
            try
            {
                new GeolocationRequest(GeolocationAccuracy.Medium);
                Location temp = await Geolocation.GetLastKnownLocationAsync();

                if (temp != null)
                {
                    return temp;
                }
                throw new Exception("Could not get current location.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public double GetDistanceBetween(Location location1, Location location2)
        {
            double value = LocationExtensions.CalculateDistance(location1, location2, UNITS);
            return value;
        }
        public static double DistanceBetween(Location location1, Location location2)
        {
            double value = LocationExtensions.CalculateDistance(location1, location2, UNITS);
            return value;
        }
        
        public async void UpdateDistance()
        {
            Thread.Sleep(time);
            
   
            Location location1 = await GetCurrentLocation();
            Location location2 = new Location();
            double distance = 0;
            fromDatabase = await App.DatabaseHelper.GetRemindersAsync(4);
            foreach (Reminder rem in fromDatabase)
            {
                location2.Latitude = rem.Latitude;
                location2.Longitude = rem.Longitude;
                distance = GetDistanceBetween(location1, location2);
                rem.DistanceToDestination = distance;
                if(distance < rem.Range)
                {
                    //trigger notification
                    NotificationHelper.pushNotification(rem);
                }
                await App.DatabaseHelper.UpdateReminderAsync(rem);
            }
            
        }
        
    }
}
