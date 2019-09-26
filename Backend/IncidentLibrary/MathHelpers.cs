using System;

namespace IncidentLibrary
{
    /// <summary>
    /// Additional helper math functions.
    /// </summary>
    public class MathHelpers
    {
        /// <summary>
        /// Convert degrees to radians.
        /// (Using https://stackoverflow.com/questions/4164830/)
        /// </summary>
        /// <param name="angle">The angle in degrees</param>
        /// <returns>The angle in radians</returns>
        public static double DegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// Convert radians to degrees.
        /// (Using http://www.vcskicks.com/csharp_net_angles.php)
        /// </summary>
        /// <param name="angle">The angle in radians</param>
        /// <returns>The angle in degrees</returns>
        public static double RadiansToDegrees(double angle) {
            return angle * (180.0 / Math.PI);
        }
    }
}