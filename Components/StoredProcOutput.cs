using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    public class StoredProcOutput
    {
        public int SPStatus { get; set; }
        public string ErrMsg { get; set; }
    }
}