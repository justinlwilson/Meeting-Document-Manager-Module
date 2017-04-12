using System;
using ForsythCo.Modules.MeetingDocumentManager;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class AddVideo : MeetingDocumentManagerModuleBase
    {
        private MeetingController meetCon = new MeetingController();
        private MarkerController markerCon = new MarkerController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ItemId == -1)
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());

            if(!UserInfo.IsInRole("VideoEditors"))
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to add videos to meetings.");

            if (!Page.IsPostBack)
            {
                txtVimeo.Text = meetCon.GetMeetingByID(ItemId).VimeoNumber;
            }
        }

        protected void RadGrid_NeedData(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            (sender as RadGrid).DataSource = meetCon.GetMeetingByID(ItemId).Markers;
        }

        protected void btnAddVideo_Click(object sender, EventArgs e)
        {
            Meeting _thisMeeting = meetCon.GetMeetingByID(ItemId);
            _thisMeeting.VimeoNumber = txtVimeo.Text;
            meetCon.UpdateMeeting(_thisMeeting, UserId);
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnAddBookmark_Click(object sender, EventArgs e)
        {
            TimeSpan _time = new TimeSpan(Convert.ToInt32(txtHour.Text), Convert.ToInt32(txtMin.Text), Convert.ToInt32(txtSec.Text));


            markerCon.AddMarker(new Marker()
            {
                MeetingID = ItemId,
                KeyTime = Convert.ToInt32(_time.TotalSeconds),
                ShortDescription = txtTitle.Text
            });

            grdBookmarks.DataSource = meetCon.GetMeetingByID(ItemId).Markers;
            grdBookmarks.DataBind();

            txtHour.Text = "0";
            txtMin.Text = "0";
            txtSec.Text = "0";
            txtTitle.Text = "";
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item) 
            {
                int markerID = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MarkerID"]);

                markerCon.DeleteMarker(markerCon.GetMarkerByMarkerID(markerID));
            }
        }

    }
}