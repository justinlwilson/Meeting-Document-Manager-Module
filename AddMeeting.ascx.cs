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
                    datePicker.Text = _m.Begining.ToShortDateString();
                    txtMin.Text = _m.Begining.Minute.ToString();
                    if (_m.Begining.Hour > 12)
                    {
                        txtHour.Text = (_m.Begining.Hour - 12).ToString();
                        ddAMPM.SelectedIndex = 1;
                    }
                    else
                    {
                        txtHour.Text = _m.Begining.Hour.ToString();
                        ddAMPM.SelectedIndex = 0;
                    }
                    //timePicker.SelectedTime = _m.Begining.TimeOfDay;
                    //datePicker.SelectedDate = _m.Begining;
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
            DateTime beginingDate = Convert.ToDateTime(datePicker.Text);
            TimeSpan ts;

            if (ddAMPM.SelectedIndex == 1)
            {
                ts = new TimeSpan(Convert.ToInt32(txtHour.Text) + 12, Convert.ToInt32(txtMin.Text), 0);
            }
            else
            {
                ts = new TimeSpan(Convert.ToInt32(txtHour.Text), Convert.ToInt32(txtMin.Text), 0);
            }
            beginingDate = beginingDate.Date + ts;
            //beginingDate = null;
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
            //datePicker.Clear();
            //timePicker.Clear();
        }
    }
}