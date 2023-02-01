using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Skolplattformen.ElevApp.DexterApi.Models.Internal
{
    internal class Link
    {

        [JsonPropertyName("href")]
        public string? Href { get; set; }

        [JsonPropertyName("rel")]
        public string? Rel { get; set; }
    }

    internal class RoleData
    {
        [JsonPropertyName("unitName")]
        public string? UnitName { get; set; }

        [JsonPropertyName("roleName")]
        public string? RoleName { get; set; }

        [JsonPropertyName("defaultRole")]
        public bool DefaultRole { get; set; }

        [JsonPropertyName("roleType")]
        public string? RoleType { get; set; }

        [JsonPropertyName("link")]
        public List<Link> Link { get; set; } = new List<Link>();
    }

    internal class GetRolesResponse
    {
        [JsonPropertyName("roleData")]
        public List<RoleData> RoleData { get; set; } = new List<RoleData>();
    }


    internal class GetStudentsResponse
    {
        [JsonPropertyName("student")]
        public List<Student> Student { get; set; }
    }

    internal class Student
    {
        [JsonPropertyName("socialSecurityNumber")]
        public string? SocialSecurityNumber { get; set; }

        [JsonPropertyName("firstName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("unitName")]
        public string? UnitName { get; set; }

        [JsonPropertyName("unitDescription")]
        public string? UnitDescription { get; set; }

        [JsonPropertyName("schoolOperation")]
        public string? SchoolOperation { get; set; }

        [JsonPropertyName("lastName")]
        public string? LastName { get; set; }

        [JsonPropertyName("link")]
        public List<Link> Link { get; set; } = new List<Link>();
    }



    internal class GetScheduleResponse
    {
        [JsonPropertyName("scheduleEvent")]
        public List<ScheduleEvent> ScheduleEvent { get; set; }
    }

    internal class ScheduleEvent
    {
        [JsonPropertyName("startTime")]
        public DateTime? StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTime? EndTime { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("course")]
        public string Course { get; set; }

        [JsonPropertyName("reportPresence")]
        public object ReportPresence { get; set; }

        [JsonPropertyName("roomName")]
        public string RoomName { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("effectiveTime")]
        public int? EffectiveTime { get; set; }

        [JsonPropertyName("link")]
        public List<Link> Link { get; set; }
    }




}
