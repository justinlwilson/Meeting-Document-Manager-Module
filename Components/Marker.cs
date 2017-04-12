using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Markers")]
    [PrimaryKey("MarkerID", AutoIncrement = true)]
    [Cacheable("Markers", CacheItemPriority.Default, 20)]
    public class Marker
    {
        private int _KeyTime;
        public int MarkerID { get; set; }
        public int MeetingID { get; set; }
        public int KeyTime 
        { 
            get 
            { 
                return _KeyTime; 
            }
            set 
            { 
                _KeyTime = value;
                DateTime start = new DateTime();
                DateTime end = start.AddSeconds(value);
                this.Duration = new TimeSpan(end.Ticks - start.Ticks);

                if (this.Duration.Hours == 0)
                    this.TimeString = string.Format("{0:00}:{1:00}", this.Duration.Minutes, this.Duration.Seconds);
                else
                    this.TimeString = string.Format("{0:00}:{1:00}:{2:00}", this.Duration.Hours, this.Duration.Minutes, this.Duration.Seconds);
            }
        }
        public string ShortDescription { get; set; }
        [IgnoreColumn]
        public TimeSpan Duration { get; set; }
        [IgnoreColumn]
        public string TimeString { get; set; }

    }
}