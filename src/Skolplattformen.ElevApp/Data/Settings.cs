using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skolplattformen.ElevApp.Data
{
    internal static class Settings
    {
        public static string SkolmatenSeSchoolName
        {
            get => Preferences.Get("Settings_SkolmatenSeSchoolName", null);
            set => Preferences.Default.Set("Settings_SkolmatenSeSchoolName", value);
        }

        public static bool UseSkolmatenSe
        {
            get
            {
                var school = Preferences.Get("Settings_SkolmatenSeSchoolName", null);
                var useSchool = Preferences.Get("Settings_UseSkolmatenSe", false);

                return !string.IsNullOrEmpty(school) && useSchool;
            }
            set => Preferences.Default.Set("Settings_UseSkolmatenSe", value);
        }

        public static bool ShowCalendarInTodayView
        {
            get => Preferences.Get("Settings_ShowCalendarInTodayView", true);
            set => Preferences.Default.Set("Settings_ShowCalendarInTodayView", value);
        }

        public static bool ShowPlannedAbsenceInTodayView
        {
            get => Preferences.Get("Settings_ShowPlannedAbsenceInTodayView", true);
            set => Preferences.Default.Set("Settings_ShowPlannedAbsenceInTodayView", value);
        }

        public static bool ShowKalendariumInTodayView
        {
            get => Preferences.Get("Settings_ShowKalendariumInTodayView", true);
            set => Preferences.Default.Set("Settings_ShowKalendariumInTodayView", value);
        }

    }
}
