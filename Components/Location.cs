using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Locations")]
    [PrimaryKey("LocationID", AutoIncrement = true)]
    [Cacheable("Locations", CacheItemPriority.Default, 20)]
    public class Location
    {
        public int LocationID { get; set; }
        public string BuildingName { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int UserID { get; set; }
        [IgnoreColumn]
        public int SPStatus { get; set; }
        [IgnoreColumn]
        public string ErrMsg { get; set; }
    }
}