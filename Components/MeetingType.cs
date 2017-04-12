using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_MeetingTypes")]
    [PrimaryKey("MeetingTypeID", AutoIncrement = true)]
    [Cacheable("MeetingTypes", CacheItemPriority.Default, 20)]
    public class MeetingType
    {
        public int MeetingTypeID { get; set; }
        public string TypeName { get; set; }
        public string ShortDescription { get; set; }
        public int SortOrder { get; set; }

    }
}