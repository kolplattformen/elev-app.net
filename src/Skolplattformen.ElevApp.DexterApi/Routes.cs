namespace Skolplattformen.ElevApp.DexterApi
{
    internal static class Routes
    {
        public static string GetRoles => "users/0/roles";
        public static string GetUser => "users/0";
        public static string GetStudents => "school/student/0/students";

        public static string GetAttendanceReasons(string userId) => $"school/student/0/students/{userId}/attendancereasons";
        public static string GetAbsenceAccourances(string userId, DateTime startTime, DateTime stopTime) => 
            $"school/student/0/students/{userId}/absenceoccurrences?startTime={startTime}&stopTime={stopTime}";

        public static string GetSchedule(string userId, DateTime startTime, DateTime stopTime) =>
            $"school/student/0/students/{userId}/schedule?startTime={startTime.ToString("o")}&stopTime={stopTime.ToString("o")}";

    }
}
