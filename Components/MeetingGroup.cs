using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_MeetingGroups")]
    [PrimaryKey("MeetingGroupID", AutoIncrement=true)]
    [Cacheable("MeetingGroups", CacheItemPriority.Default, 20)]
    public class MeetingGroup
    {
        public int MeetingGroupID { get; set; }
        public string GroupName { get; set; }
        public string ShortDescription { get; set; }
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