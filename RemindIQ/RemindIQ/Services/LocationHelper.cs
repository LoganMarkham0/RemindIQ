using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using RemindIQ.Models;

namespace RemindIQ.Services
{
    public class LocationHelper
    {
        private static readonly DistanceUnits UNITS = DistanceUnits.Miles;
        List<Reminder> fromDatabase;
        public LocationHelper()
        {
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
        public double GetDistanceBetween(Location location1, Location location2)
        {
            double value = LocationExtensions.CalculateDistance(location1, location2, UNITS);
            value = Math.Round(value, 1);
            return value;
        }
        public async void UpdateAllDistances()
        {  
            Location location1 = await GetCurrentLocation();
            Location location2 = new Location();
            double distance;
            fromDatabase = await App.databaseHelper.GetRemindersAsync(4);
            foreach (Reminder rem in fromDatabase)
            {
                location2.Latitude = rem.Latitude;
                location2.Longitude = rem.Longitude;
                distance = GetDistanceBetween(location1, location2);
                rem.DistanceToDestination = distance;
                if (rem.HasBeenNotified)
                {
                    if(rem.DistanceToDestination > rem.Range)
                    {
                        rem.Status = 1;
                    }
                    if(rem.DistanceToDestination <= rem.Range)
                    {
                        //They've been notified and are still in range.
                    }
                }
                if (!rem.HasBeenNotified)
                {
                    if (rem.DistanceToDestination > rem.Range)
                    {
                        //They've not been notified and are not in range.
                    }
                    if (rem.DistanceToDestination <= rem.Range)
                    {
                        //They've not been notified and are in range.
                        App.notificationHelper.pushNotification(rem);
                        rem.HasBeenNotified = true;
                    }
                }
                await App.databaseHelper.UpdateReminderAsync(rem);
            }
        }
    }
}