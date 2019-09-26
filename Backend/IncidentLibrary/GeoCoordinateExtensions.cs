using System;
using GeoCoordinatePortable;

using static IncidentLibrary.MathHelpers;

namespace IncidentLibrary
{
    /// <summary>
    /// Helper methods to add to GeoCoordinate.
    /// </summary>
    public static class GeoCoordinateExtensions
    {
        /// <summary>
        /// Calculate the mid point of two coordinates.
        /// (Using https://stackoverflow.com/questions/4164830/)
        /// </summary>
        /// <param name="posA">The first coordinate</param>
        /// <param name="posB">The second coordinate</param>
        /// <returns>The midpoint coordinate</returns>
        public static GeoCoordinate MidPoint(this GeoCoordinate posA, GeoCoordinate posB)
        {
            var midPoint = new GeoCoordinate();

            double dLon = DegreesToRadians(posB.Longitude - posA.Longitude);
            double Bx = Math.Cos(DegreesToRadians(posB.Latitude)) * Math.Cos(dLon);
            double By = Math.Cos(DegreesToRadians(posB.Latitude)) * Math.Sin(dLon);

            midPoint.Latitude = RadiansToDegrees(Math.Atan2(
                            Math.Sin(DegreesToRadians(posA.Latitude)) + Math.Sin(DegreesToRadians(posB.Latitude)),
                            Math.Sqrt(
                                (Math.Cos(DegreesToRadians(posA.Latitude)) + Bx) *
                                (Math.Cos(DegreesToRadians(posA.Latitude)) + Bx) + By * By)));

            midPoint.Longitude = posA.Longitude + RadiansToDegrees(Math.Atan2(By, Math.Cos(DegreesToRadians(posA.Latitude)) + Bx));

            return midPoint;
        }
    }
}