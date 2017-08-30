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
                    pnlAdminAddMeeting.Visible = true;
                    lnkAddMeeting.NavigateUrl = EditUrl(string.Empty, string.Empty, "Meetings");

                    hdnEditAlert.Value = EditUrl(string.Empty, string.Empty, "Alert", "tid=");
                    hdnEditVideo.Value = EditUrl(string.Empty, string.Empty, "Video", "tid=");
                    hdnEditDoc.Value = EditUrl(string.Empty, string.Empty, "Documents", "tid=");
                    hdnEditMeeting.Value = EditUrl(string.Empty, string.Empty, "Meetings", "tid=");

                    hdnIsEditor.Value = "true";
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

        protected int CountDocuments(IEnumerable<Document> d)
        {
            int v = 0;
            foreach (Document doc in d)
            {
                v++;
            }
            return v;
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