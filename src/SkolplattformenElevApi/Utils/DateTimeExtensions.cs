using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SkolplattformenElevApi.Utils
{
    internal static class DateTimeExtensions
    {
        private static string _tzId;
        internal static DateTime ToCEST(this DateTime utcDateTime)
        {
            if (string.IsNullOrEmpty(_tzId))
            {
                //// iOS uses "Europe/Stockholm", Android and Windows has "Central European Standard Time"
                _tzId = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.Id.Contains("Europe/Stockholm"))?.Id ?? "Central European Standard Time";
            }
            
            TimeZoneInfo cestTz = TimeZoneInfo.FindSystemTimeZoneById(_tzId);
            DateTime cest = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, cestTz);
            return cest;
        }

        internal static DateTime FromCEST(this DateTime cestDateTime)
        {
            if (string.IsNullOrEmpty(_tzId))
            {
                //// iOS uses "Europe/Stockholm", Android and Windows has "Central European Standard Time"
                _tzId = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(x => x.Id.Contains("Europe/Stockholm"))?.Id ?? "Central European Standard Time";
            }

            TimeZoneInfo cestTz = TimeZoneInfo.FindSystemTimeZoneById(_tzId);

            DateTime utc = TimeZoneInfo.ConvertTimeToUtc(cestDateTime, cestTz);
            return utc;
        }

    }
}
