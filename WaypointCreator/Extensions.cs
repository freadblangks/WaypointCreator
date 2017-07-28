#region

using System;

#endregion

namespace WaypointCreator
{
    public static class Extensions
    {
        #region Public Methods and Operators

        public static bool IsNumeric(this string val)
        {
            double temp;
            return Double.TryParse(val, out temp);
        }

        public static float ToFloat(this string val)
        {
            float result;

            if (float.TryParse(val, out result))
            {
                return result;
            }

            return 0f;
        }

        public static TimeSpan ToTimeSpan(this string val)
        {
            TimeSpan result;

            if (TimeSpan.TryParse(val, out result))
            {
                return result;
            }

            return new TimeSpan();
        }

        public static uint ToUint(this string val)
        {
            uint result;

            if (uint.TryParse(val, out result))
            {
                return result;
            }

            return 0u;
        }

        #endregion
    }
}