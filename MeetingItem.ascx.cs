using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class MeetingItem : MeetingDocumentManagerModuleBase
    {
        private bool _iseditable = false;

        public int ModuleID { get; set; }

        public string CalendarAPI { get; set; }
        public bool IsEditable { get { return _iseditable; } set { _iseditable = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadMeeting(Meeting m)
        {
            DocumentDisplay.IsEditable = this.IsEditable;
            DocumentDisplay.ModuleID = this.ModuleID;
            if (m.Begining > DateTime.Now) {
                lblCal.Visible = true;
                lblCal.Text = "<a rel='tooltip' data-original-title='Save Event to Calendar' href='" + ResolveClientUrl("~/DesktopModules/MeetingDocumentManager/api/meetingapi/Calendar") + "?id=" + m.MeetingID + "'><em class='icon icon-calendar'></em></a>";
            }

            if (!String.IsNullOrEmpty(m.Flag)) 
            {
                lblCal.Visible = false;
                lblCancelled.Visible = true;
                lblCancelled.Text = "<div class='alert alert-danger'><strong>NOTICE: </strong>" + m.Flag + "</div>";
            }
            lblMeetingHead.Text = String.Format("{0} - {1}", m.MeetingGroup.GroupName, m.MeetingType.TypeName);
            lblMeetingDate.Text = m.Begining.ToLongDateString() + " " + m.Begining.ToString("t");
            GoogleMap.LoadMap(m.Location);
            DocumentDisplay.LoadDocuments(m);
            if (!string.IsNullOrEmpty(m.VimeoNumber))
            {
                VideoDisplay.LoadVimeo(m.VimeoNumber);
                VideoDisplay.LoadMarkers(m.Markers);
            }
            else
                VideoDisplay.Visible = false;
        }
    }
}