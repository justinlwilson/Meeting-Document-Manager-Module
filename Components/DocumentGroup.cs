using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_DocumentGroups")]
    [PrimaryKey("DocumentGroupID", AutoIncrement=true)]
    [Cacheable("DocumentGroups", CacheItemPriority.Default, 20)]
    public class DocumentGroup
    {
        public int DocumentGroupID { get; set; }
        public string GroupName { get; set; }
        public string ShortDescription { get; set; }
        public int SortOrder { get; set; }

    }
}