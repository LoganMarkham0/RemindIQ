using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RemindIQ.Services
{
    public class LocationHelper
    {
        private static readonly DistanceUnits UNITS = DistanceUnits.Miles;
        public LocationHelper( )
        {

        }

        public async Task<Location> GetCoordsByAddress(string address)
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
            catch (FeatureNotSupportedException fnsEx)
            {
                throw fnsEx;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
            catch (FeatureNotSupportedException fnsEx)
            {
                throw fnsEx;
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
                else
                {
                    throw new Exception("Could not get current location.");
                }
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
    }
}
