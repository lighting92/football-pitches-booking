using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballPitchesBooking.Utilities
{
    public class Utils
    {
        public int DoubleToMinute(double min)
        {
            return (int)(60 * min);
        }

        public double MinuteToDouble(int min)
        {
            return (min / 60);
        }

        public double MinuteToDouble(string minute)
        {
            int min;
            if (Int32.TryParse(minute, out min))
            {
                return (min / 60);
            }
            return 0;
        }
    }
}