using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System.Collections.Generic;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Meetings")]
    [PrimaryKey("MeetingID", AutoIncrement = true)]
    [Cacheable("Meetings", CacheItemPriority.Normal, 20)]
    public class Meeting
    {
        public int MeetingID { get; set; }
        public int MeetingGroupID { get; set; }
        public int LocationID { get; set; }
        public int MeetingTypeID { get; set; }
        public string VimeoNumber { get; set; }
        public string Flag { get; set; }
        public DateTime Begining { get; set; }
        public DateTime RecordCreated { get; set; }
        public DateTime RecordUpdated { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool Published { get; set; }

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

        [IgnoreColumn]
        public MeetingType MeetingType
        {
            get
            {
                return new MeetingTypeController().GetMeetingTypeByID(this.MeetingTypeID);
            }
        }

        [IgnoreColumn]
        public Location Location
        {
            get
            {
                return new LocationController().GetLocation(this.LocationID);
            }
        }

        [IgnoreColumn]
        public MeetingGroup MeetingGroup
        {
            get
            {
                return new MeetingGroupController().GetMeetingGroupByID(this.MeetingGroupID);
            }
        }

        [IgnoreColumn]
        public string DateString
        {
            get
            {
                return this.Begining.ToShortDateString();
            }
        }

        [IgnoreColumn]
        public string TimeString
        {
            get
            {
                return this.Begining.ToShortTimeString();
            }
        }

        [IgnoreColumn]
        public string MonthYearGroup
        {
            get
            {
                return this.Begining.ToString("MMMM yyyy");
            }
        }

        [IgnoreColumn]
        public DateTime MonthYearGroupDT
        {
            get
            {
                return new DateTime(this.Begining.Year, this.Begining.Month, 1);
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