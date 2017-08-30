using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Meetings")]
    [PrimaryKey("MeetingID", AutoIncrement = true)]
    [Cacheable("Meetings", CacheItemPriority.Normal, 20)]
    [JsonObject(MemberSerialization.OptIn)]
    public class Meeting
    {
        [JsonProperty]
        public int MeetingID { get; set; }
        [JsonProperty]
        public int MeetingGroupID { get; set; }
        [JsonProperty]
        public int LocationID { get; set; }
        [JsonProperty]
        public int MeetingTypeID { get; set; }
        [JsonProperty]
        public string VimeoNumber { get; set; }
        [JsonProperty]
        public string Flag { get; set; }
        [JsonProperty]
        public DateTime Begining { get; set; }
        [JsonProperty]
        public DateTime RecordCreated { get; set; }
        [JsonProperty]
        public DateTime RecordUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool Published { get; set; }
        [JsonProperty]
        [IgnoreColumn]
        public string FormattedDescription
        {
            get
            {
                return string.Format("{0} - {1} {2}", this.MeetingGroup.GroupName, this.MeetingType.TypeName, this.Begining.ToShortTimeString());
            }
        }

        [IgnoreColumn]
        public IEnumerable<Document> Documents
        {
            get
            {
                return new DocumentController().GetDocuments(this.MeetingID);
            }
        }

        [IgnoreColumn]
        public IEnumerable<Marker> Markers
        {
            get
            {
                return new MarkerController().GetMarkers(this.MeetingID);
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public MeetingType MeetingType
        {
            get
            {
                return new MeetingTypeController().GetMeetingTypeByID(this.MeetingTypeID);
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public Location Location
        {
            get
            {
                return new LocationController().GetLocation(this.LocationID);
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public MeetingGroup MeetingGroup
        {
            get
            {
                return new MeetingGroupController().GetMeetingGroupByID(this.MeetingGroupID);
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public string DateString
        {
            get
            {
                return this.Begining.ToShortDateString();
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public string TimeString
        {
            get
            {
                return this.Begining.ToShortTimeString();
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public string MonthYearGroup
        {
            get
            {
                return this.Begining.ToString("MMMM yyyy");
            }
        }
        [JsonProperty]
        [IgnoreColumn]
        public DateTime MonthYearGroupDT
        {
            get
            {
                return new DateTime(this.Begining.Year, this.Begining.Month, 1);
            }
        }

        [JsonProperty]
        [IgnoreColumn]
        public string AlertURL
        {
            get
            {
                return new MeetingDocumentManagerModuleBase().EditUrl(string.Empty, string.Empty, "Alert", "tid=" + this.MeetingID);
            }
        }

        [JsonProperty]
        [IgnoreColumn]
        public string VideoURL
        {
            get
            {
                return new MeetingDocumentManagerModuleBase().EditUrl(string.Empty, string.Empty, "Video", "tid=" + this.MeetingID);
            }
        }

        [JsonProperty]
        [IgnoreColumn]
        public string DocumentsURL
        {
            get
            {
                return new MeetingDocumentManagerModuleBase().EditUrl(string.Empty, string.Empty, "Documents", "tid=" + this.MeetingID);
            }
        }

        [JsonProperty]
        [IgnoreColumn]
        public string MeetingURL
        {
            get
            {
                return new MeetingDocumentManagerModuleBase().EditUrl(string.Empty, string.Empty, "Meetings", "tid=" + this.MeetingID);
            }
        }
    }

    public class MeetingSP
    {
        public int MeetingID { get; set; }
        public int MeetingGroupID { get; set; }
        public int LocationID { get; set; }
        public int MeetingTypeID { get; set; }
        public string VimeoNumber { get; set; }
        public string Flag { get; set; }
        public DateTime Begining { get; set; }
        public bool Published { get; set; }
        public int UserID { get; set; }
        public int SPStatus { get; set; }
        public string ErrMsg { get; set; }
    }

}