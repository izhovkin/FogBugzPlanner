using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FogBugzPlanner.Client
{
    internal static class Extensions
    {
        public static int ToInt(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            try
            {
                return (int)Convert.ChangeType(s, typeof(int), CultureInfo.InvariantCulture);
            }
            catch(FormatException)
            {
                return 0;
            }            
        }
    }
}
