using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Audio_MIME")]
    [PrimaryKey("MIMEID", AutoIncrement=true)]
    [Cacheable("AudioMIME", CacheItemPriority.Default, 20)]
    public class AudioMIME
    {
        public int MIMEID { get; set; }
        public string Extension { get; set; }
        public string MIME { get; set; }
    }
}