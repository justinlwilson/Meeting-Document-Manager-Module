using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_MeetingGroups")]
    [PrimaryKey("MeetingGroupID", AutoIncrement=true)]
    [Cacheable("MeetingGroups", CacheItemPriority.Default, 20)]
    [JsonObject(MemberSerialization.OptIn)]
    public class MeetingGroup
    {
        [JsonProperty(propertyName:"id")]
        public int MeetingGroupID { get; set; }
        [JsonProperty(propertyName: "name")]
        public string GroupName { get; set; }
        [JsonProperty(propertyName: "description")]
        public string ShortDescription { get; set; }
        [JsonProperty(propertyName: "sort")]
        public int SortOrder { get; set; }

        [IgnoreColumn]
        public IEnumerable<Meeting> MeetingList
        {
            get
            {
                return new MeetingController().GetByMeetingGroup(this);
            }
        }
        
    }
}