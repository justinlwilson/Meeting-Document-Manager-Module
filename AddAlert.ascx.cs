using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class AddAlert : MeetingDocumentManagerModuleBase
    {
        MeetingController meetCon = new MeetingController();
        Meeting _meeting;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserInfo.IsInRole("MeetingEditors"))
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to add documents to meetings.  Please contact your System Administrator.");

            if (!Page.IsPostBack)
            {
                ddAlerts.DataSource = new MDMAlertController().GetAlerts();
                ddAlerts.DataBind();
            }

            if (ItemId == -1)
            { 
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=No meeting specified.  Please contact your System Administrator."); 
            }
            else
            {
                _meeting = meetCon.GetMeetingByID(ItemId);
                if (!String.IsNullOrEmpty(_meeting.Flag))
                {
                    ddAlerts.Items.FindByText(_meeting.Flag).Selected = true;
                    btnClear.Visible = true;
                }
                    
            }
        }

        protected void btnAddAlert_Click(object sender, EventArgs e)
        {
            _meeting.Flag = ddAlerts.SelectedValue;
            meetCon.UpdateMeeting(_meeting, UserId);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            _meeting.Flag = "";
            meetCon.UpdateMeeting(_meeting, UserId);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());

        }
    }
}