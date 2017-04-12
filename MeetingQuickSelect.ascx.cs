using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class MeetingQuickSelect : System.Web.UI.UserControl
    {

        private int _meetingID = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
            }
        }

        protected void cal_SelectionChanged(object sender, EventArgs e)
        {
            rblMeetings.DataSource = new MeetingDocumentManager.Components.MeetingController().GetByDate(cal.SelectedDate);
            rblMeetings.DataBind();
        }

        protected void rblMeetings_SelectedIndexChanged(object sender, EventArgs e)
        {
            _meetingID = Convert.ToInt32(rblMeetings.SelectedValue);
        }

        public int MeetingID { get { return _meetingID; } }
    }
}