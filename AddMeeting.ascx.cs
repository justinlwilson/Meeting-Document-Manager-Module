using System;
using DotNetNuke.Entities.Users;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Services.Exceptions;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class AddMeeting : MeetingDocumentManagerModuleBase
    {
        private MeetingGroupController mgCon = new MeetingGroupController();
        private LocationController loCon = new LocationController();
        private MeetingTypeController mtCon = new MeetingTypeController();
        private MeetingController mCon = new MeetingController();

        private bool isEdit = false;
        private Meeting _m;
        protected void Page_Load(object sender, EventArgs e)
        {
            
                if (!UserInfo.IsInRole("MeetingEditors"))
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to add documents to meetings.  Please contact your System Administrator.");

                if (!Page.IsPostBack) 
                {
                    PopulateDropDowns();
                    timePicker.TimeView.Interval = new TimeSpan(0,30,0);
                    timePicker.TimeView.StartTime = new TimeSpan(6, 0, 0);
                    timePicker.TimeView.EndTime = new TimeSpan(22, 0, 0);
                    timePicker.TimeView.Columns = 4;
                }

                if (ItemId != -1)
                {
                    isEdit = true;
                    _m = mCon.GetMeetingByID(ItemId);
                    if (!Page.IsPostBack)
                    {
                        ddLocation.Items.FindByValue(_m.LocationID.ToString()).Selected = true;
                        ddMeetingGroup.Items.FindByValue(_m.MeetingGroupID.ToString()).Selected = true;
                        ddMeetingType.Items.FindByValue(_m.MeetingTypeID.ToString()).Selected = true;

                        timePicker.SelectedTime = _m.Begining.TimeOfDay;
                        datePicker.SelectedDate = _m.Begining;
                    }
                    
                }
            
        }

        protected void PopulateDropDowns()
        {
            ddMeetingGroup.DataSource = mgCon.GetMeetingGroups();
            ddMeetingGroup.DataBind();

            ddMeetingType.DataSource = mtCon.GetMeetingTypes();
            ddMeetingType.DataBind();

            ddLocation.DataSource = loCon.GetLocations();
            ddLocation.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnAddMeeting_Click(object sender, EventArgs e)
        {
            DateTime beginingDate = Convert.ToDateTime(datePicker.SelectedDate);
            beginingDate = beginingDate.Add((TimeSpan)timePicker.SelectedTime);
            if(isEdit)
            {
                _m.MeetingGroupID = Convert.ToInt32(ddMeetingGroup.SelectedValue);
                _m.MeetingTypeID = Convert.ToInt32(ddMeetingType.SelectedValue);
                _m.LocationID = Convert.ToInt32(ddLocation.SelectedValue);
                _m.Begining = beginingDate;
                mCon.UpdateTheMeeting(_m);
            }
            else
            {
                MeetingSP m = new MeetingSP();
                m.MeetingGroupID = Convert.ToInt32(ddMeetingGroup.SelectedValue);
                m.MeetingTypeID = Convert.ToInt32(ddMeetingType.SelectedValue);
                m.LocationID = Convert.ToInt32(ddLocation.SelectedValue);
                m.Begining = beginingDate;
                m.UserID = UserId;

                mCon.SP_CreateMeeting(m);
            }
            

            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
            
        }

        protected void ClearForm()
        {
            datePicker.Clear();
            timePicker.Clear();
        }
    }
}