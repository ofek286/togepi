using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using GeoCoordinatePortable;
using HEREMaps.Base;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HEREMaps.LocationServices
{
    /// <summary>
    /// Access to the Geocoding functions of HERE Maps.
    /// </summary>
    public class Geocoding
    {
        /// <summary>
        /// An (optional) logger.
        /// </summary>
        private readonly ILogger<Geocoding> logger;

        /// <summary>
        /// HERE Maps API key.
        /// </summary>
        private readonly HEREApp apiKey;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="api">HERE Maps API key</param>
        /// <param name="loggerArg">(Optional) logger to use</param>
        public Geocoding(HEREApp api, ILogger<Geocoding> loggerArg = null)
        {
            logger = loggerArg;
            apiKey = api;
        }

        /// <summary>
        /// Perform geocode function (location string to coordinates).
        /// </summary>
        /// <param name="query">The location string</param>
        /// <returns>The geocoordinates of the location</returns>
        public async Task<GeoCoordinate> Geocode(string query)
        {
            // Clean query
            query = HttpUtility.UrlEncode(query, Encoding.UTF8);

            // Create the request
            var requestResult = await CURL.GET("https://geocoder.api.here.com/6.2/geocode.json",
                new Dictionary<string, string> {
                    { "app_id", apiKey.AppId },
                    { "app_code", apiKey.AppCode },
                    { "searchtext", query }
                });

            // Send the request and extract geocoordinates
            try
            {
                dynamic resultObj = JsonConvert.DeserializeObject(requestResult);
                var view = resultObj.Response.View;
                var loc = view[0].Result[0].Location.DisplayPosition;
                return new GeoCoordinate
                {
                    Latitude = loc.Latitude,
                    Longitude = loc.Longitude
                };
            }
            catch (JsonException ex)
            {
                logger?.LogError("Geocode error: " + ex.Message);
                return GeoCoordinate.Unknown;
            }
        }

        /// <summary>
        /// Perform reverse geocode function (coordinates to location string).
        /// </summary>
        /// <param name="loc">The location's geocoordinates</param>
        /// <param name="radius">(Optional) result accuracy radius</param>
        /// <returns>The string description of the location</returns>
        public async Task<string> ReverseGeocode(GeoCoordinate loc, double radius = 100)
        {
            // Create the request
            var requestResult = await CURL.GET("https://reverse.geocoder.api.here.com/6.2/reversegeocode.json",
                new Dictionary<string, string> {
                    { "app_id", apiKey.AppId },
                    { "app_code", apiKey.AppCode },
                    { "prox", loc.Latitude + "," + loc.Longitude + "," + radius },
                    { "mode", "retrieveAddresses" },
                    { "maxresults", "1" }
                });

            // Send the request and extract the location string
            try
            {
                dynamic resultObj = JsonConvert.DeserializeObject(requestResult);
                var view = resultObj.Response.View[0];
                var res = view.Result[0];
                return res.Location.Address.Label;
            }
            catch (JsonException ex)
            {
                logger?.LogError("Reverse geocode error: " + ex.Message);
                return null;
            }
        }

        #region Shortcuts
        /// <summary>
        /// Perform geocode function (location string to coordinates).
        /// </summary>
        /// <param name="apiKey">HERE Maps API key</param>
        /// <param name="query">The location string</param>
        /// <returns>The geocoordinates of the location</returns>
        public static async Task<GeoCoordinate> Geocode(HEREApp apiKey, string query)
        {
            var geocoding = new Geocoding(apiKey);
            return await geocoding.Geocode(query);
        }

        /// <summary>
        /// Perform reverse geocode function (coordinates to location string).
        /// </summary>
        /// <param name="apiKey">HERE Maps API key</param>
        /// <param name="loc">The location's geocoordinates</param>
        /// <param name="radius">(Optional) result accuracy radius</param>
        /// <returns>The string description of the location</returns>
        public static async Task<string> ReverseGeocode(HEREApp apiKey, GeoCoordinate loc, double radius = 100)
        {
            var geocoding = new Geocoding(apiKey);
            return await geocoding.ReverseGeocode(loc, radius);
        }
        #endregion
    }
}