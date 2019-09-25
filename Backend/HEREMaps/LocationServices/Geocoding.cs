using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GeoCoordinatePortable;
using HEREMaps.Base;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HEREMaps.LocationServices {
    public class Geocoding {
        private readonly ILogger<Geocoding> logger;
        private readonly HEREApp apiKey;

        public Geocoding(HEREApp api, ILogger<Geocoding> loggerArg = null) {
            logger = loggerArg;
            apiKey = api;
        }
        public async Task<GeoCoordinate> Geocode(string query) {
            query = HttpUtility.UrlEncode(query, Encoding.UTF8);
            var requestResult = await CURL.GET("https://geocoder.api.here.com/6.2/geocode.json",
                new Dictionary<string, string> { { "app_id", apiKey.AppId },
                    { "app_code", apiKey.AppCode },
                    { "searchtext", query }
                });
            try {
                dynamic resultObj = JsonConvert.DeserializeObject(requestResult);
                var view = resultObj.Response.View;
                var loc = view[0].Result[0].Location.DisplayPosition;
                return new GeoCoordinate {
                    Latitude = loc.Latitude,
                        Longitude = loc.Longitude
                };
            } catch (JsonException ex) {
                logger?.LogError("Geocode error: " + ex.Message);
                return GeoCoordinate.Unknown;
            }
        }

        public async Task<string> ReverseGeocode(GeoCoordinate loc, double radius = 100) {
            var requestResult = await CURL.GET("https://reverse.geocoder.api.here.com/6.2/reversegeocode.json",
                new Dictionary<string, string> { { "app_id", apiKey.AppId },
                    { "app_code", apiKey.AppCode },
                    { "prox", loc.Latitude + "," + loc.Longitude + "," + radius },
                    { "mode", "retrieveAddresses" },
                    { "maxresults", "1" }
                });
            try {
                dynamic resultObj = JsonConvert.DeserializeObject(requestResult);
                var view = resultObj.Response.View[0];
                var res = view.Result[0];
                return res.Location.Address.Label;
            } catch (JsonException ex) {
                logger?.LogError("Reverse geocode error: " + ex.Message);
                return null;
            }
        }

        #region Shortcuts
        public static async Task<GeoCoordinate> Geocode(HEREApp apiKey, string query) {
            var geocoding = new Geocoding(apiKey);
            return await geocoding.Geocode(query);
        }

        public static async Task<string> ReverseGeocode(HEREApp apiKey, GeoCoordinate loc, double radius = 100) {
            var geocoding = new Geocoding(apiKey);
            return await geocoding.ReverseGeocode(loc, radius);
        }
        #endregion
    }
}