using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using Newtonsoft.Json;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_MeetingTypes")]
    [PrimaryKey("MeetingTypeID", AutoIncrement = true)]
    [Cacheable("MeetingTypes", CacheItemPriority.Default, 20)]
    [JsonObject(MemberSerialization.OptIn)]
    public class MeetingType
    {
        [JsonProperty(propertyName:"id")]
        public int MeetingTypeID { get; set; }
        [JsonProperty(propertyName: "name")]
        public string TypeName { get; set; }
        [JsonProperty(propertyName: "description")]
        public string ShortDescription { get; set; }
        [JsonProperty(propertyName: "sort")]
        public int SortOrder { get; set; }

    }
}