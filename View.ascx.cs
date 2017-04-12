using System;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Telerik.Web.UI;
using DotNetNuke.Entities.Users;
namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class View : MeetingDocumentManagerModuleBase, IActionable
    {
        MeetingGroupController groupCon = new MeetingGroupController();
        MeetingController meetCon = new MeetingController();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string baseURL = DotNetNuke.Common.Globals.NavigateURL(TabId);
                //av.BaseURL = baseURL;
                //avNext.BaseURL = baseURL;


                if (IsEditable)
                {
                    RadGrid1.Columns[0].Visible = true;
                    pnlAdminAddMeeting.Visible = true;
                    lnkAddMeeting.NavigateUrl = EditUrl(string.Empty, string.Empty, "Meetings");
                }

                if (Meeting != -1)
                {
                    ModuleConfiguration.ModuleTitle = "";

                    pnlContent.Controls.Clear();
                    MeetingItem item = (MeetingItem)LoadControl("~/desktopmodules/MeetingDocumentManager/MeetingItem.ascx");
                    item.CalendarAPI = ResolveClientUrl("~/api/meetingapi/Calendar");
                    pnlContent.Controls.Add(item);
                    item.IsEditable = IsEditable;
                    item.ModuleID = this.ModuleId;
                    item.LoadMeeting(meetCon.GetMeetingByID(Meeting));

                    pnlAgenda.Visible = false;
                }
                else
                {
                    //DateTime _now = DateTime.Now;
                    //av.LoadDate(_now);
                        
                    //avNext.LoadDate(_now.AddMonths(1));
                   // HighlightDates(av.CurrentMeetingList);
                    //pnlAgenda.Visible = true;
                    
                    if (!Page.IsPostBack)
                    {
                        lnkRSS.NavigateUrl = ResolveClientUrl("~/api/meetingapi/RSS?s=" + PortalSettings.ActiveTab.FullUrl);
                        lnkCalendar.NavigateUrl = ResolveClientUrl("~/api/meetingapi/Calendar");
                        rptItemList.DataSource = groupCon.GetMeetingGroups();

                        rptItemList.DataBind();

                        lblJunk.Text = DotNetNuke.Common.Globals.NavigateURL(TabId);
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void RadGrid1_DataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item) {
                var _lnkAddVideo = e.Item.FindControl("lnkAddVideo") as HyperLink;
                var _lnkAddDocs = e.Item.FindControl("lnkAddDocs") as HyperLink;
                var _lblCount = e.Item.FindControl("lblDocCount") as Label;
                var _lnkAlert = e.Item.FindControl("lnkAlert") as HyperLink;
                var _lnkEditMeeting = e.Item.FindControl("lnkEditMeeting") as HyperLink;
                var t = (Meeting)e.Item.DataItem;

                if (!UserInfo.IsInRole("VideoEditors"))
                    _lnkAddVideo.Visible = false;

                if(IsEditable)
                {
                    _lblCount.Visible = true;
                    UserInfo ui = UserController.GetUserById(PortalId, t.CreatedBy);
                    if (Object.Equals(ui, null))
                        _lblCount.Text = String.Format("Added By: {0}<br/>Documents: {1}", "User Removed", CountDocuments(t.Documents));
                    else
                        _lblCount.Text = String.Format("Added By: {0}<br/>Documents: {1}", ui.DisplayName, CountDocuments(t.Documents));
                    _lnkAlert.NavigateUrl = EditUrl(string.Empty, string.Empty, "Alert", "tid=" + t.MeetingID);
                    _lnkAddVideo.NavigateUrl = EditUrl(string.Empty, string.Empty, "Video", "tid=" + t.MeetingID);
                    _lnkAddDocs.NavigateUrl = EditUrl(string.Empty, string.Empty, "Documents", "tid=" + t.MeetingID);
                    _lnkEditMeeting.NavigateUrl = EditUrl(string.Empty, string.Empty, "Meetings", "tid=" + t.MeetingID);
                }
            }
        }

        protected int CountDocuments(IEnumerable<Document> d)
        {
            int v = 0;
            foreach (Document doc in d)
            {
                v++;
            }
            return v;
        }
        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (Group != -1)
            {
                (sender as RadGrid).DataSource = meetCon.GetByMeetingGroup(Group);
            }
            else
            {
                (sender as RadGrid).DataSource = meetCon.GetAllMeetings();
            }
        }

        protected void rptItemListOnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is MeetingGroup)
            {
                MeetingGroup g = (MeetingGroup)e.Item.DataItem;
                return;
                //rptItemList.DataSource = meetCon.GetByMeetingGroup(g.MeetingGroupID);
                //rptItemList.DataBind();
            }
        }

        private void HighlightDates(IEnumerable<Meeting> list)
        {
            foreach (Meeting m in list)
            {
                //cal.SelectedDates.Add(m.Begining);
            }
        }

        public void rptItemListOnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditLocations", LocalResourceFile), "","","",
                            EditUrl("Locations"), false, SecurityAccessLevel.Admin, true, false
                        },
                        {
                            GetNextActionID(), Localization.GetString("EditMeetingTypes", LocalResourceFile), "", "", "",
                            EditUrl("MeetingTypes"), false, SecurityAccessLevel.Admin, true, false
                        },
                        {
                            GetNextActionID(), Localization.GetString("EditMeetingGroups", LocalResourceFile), "", "", "",
                            EditUrl("MeetingGroups"), false, SecurityAccessLevel.Admin, true, false
                        },
                        {
                            GetNextActionID(), Localization.GetString("EditDocumentTypes", LocalResourceFile), "", "", "",
                            EditUrl("DocumentTypes"), false, SecurityAccessLevel.Admin, true, false
                        }
                    };
                return actions;
                //,
                //{
                //    GetNextActionID(), Localization.GetString("AddMeeting", LocalResourceFile), "", "", "",
                //    EditUrl("Meetings"), false, SecurityAccessLevel.Edit, true, false
                //},
                //{
                //   GetNextActionID(), Localization.GetString("AddDocument", LocalResourceFile), "", "", "",
                //    EditUrl("Documents"), false, SecurityAccessLevel.Edit, true, false
                //}
            }
        }

        protected void cal_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            //av.LoadDate(e.NewDate);
            //avNext.LoadDate(e.NewDate.AddMonths(1));
            //HighlightDates(av.CurrentMeetingList);
        }
    }
}