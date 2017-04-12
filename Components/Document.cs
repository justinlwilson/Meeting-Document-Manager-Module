using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Documents")]
    [PrimaryKey("DocumentID", AutoIncrement=true)]
    [Cacheable("Documents", CacheItemPriority.Default, 20)]
    public class Document
    {
        public int DocumentID { get; set; }
        public int MeetingID { get; set; }
        public int DocumentGroupID { get; set; }
        public string Content { get; set; }
        public int UserID { get; set; }

        [IgnoreColumn]
        public DocumentGroup DocumentGroup
        {
            get
            {
                return new DocumentGroupController().GetDocumentGroupByID(this.DocumentGroupID);
            }
        }
    }
}