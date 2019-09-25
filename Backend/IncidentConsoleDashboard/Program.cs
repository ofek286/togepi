using System;
using HEREMaps.Base;
using HEREMaps.LocationServices;

namespace IncidentConsoleDashboard
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = new HEREApp {
                AppId = "nwVKikGG0miA826GlXkr",
                AppCode = "U_lOt-47GHaEnnNs34gJ6w"
            };
            var geoLoc = Geocoding.Geocode(apiKey, "Avtalyon").GetAwaiter().GetResult();
            Console.WriteLine(geoLoc.ToString());

            var reverseLoc = Geocoding.ReverseGeocode(apiKey, geoLoc).GetAwaiter().GetResult();
            Console.WriteLine(reverseLoc);
        }
    }
}
