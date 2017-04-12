using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class AgendaView : ForsythUserControlBase
    {

        MeetingController mCon = new MeetingController();

        public IEnumerable<Meeting> CurrentMeetingList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {

                //IEnumerable<Meeting> meetingList = mCon.GetUpcomingMeetings();

                //rptMeetings.DataSource = meetingList;
                //rptMeetings.DataBind();

                //HighlightDates(meetingList);
            }
        }

        protected void rptMeetings_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //AgendaItem item = (AgendaItem)e.Item.FindControl("AgendaItem");
                //item.TabID = this.TabID;
                //Meeting m = (Meeting)e.Item.DataItem;
                //this.MeetingID = m.MeetingID;
                ((AgendaItem)e.Item.FindControl("AgendaItem")).BaseURL = this.BaseURL;
                //((AgendaItem)e.Item.FindControl("AgendaItem")).MeetingID = ((Meeting)e.Item.DataItem).MeetingID.ToString();
            }
            //Console.WriteLine(sender.ToString());
        }

        public void LoadDate(DateTime e)
        {
            lblAgendaTitle.Text = String.Format("{0}", e.ToString("y"));
            IEnumerable<Meeting> meetingList = mCon.GetByMonth(e);
            rptMeetings.DataSource = meetingList;
            rptMeetings.DataBind();
            this.CurrentMeetingList = meetingList;
        }

        public void LoadDate(int month, int year)
        {
            DateTime e = new DateTime(year, month, 1);
            lblAgendaTitle.Text = String.Format("{0}", e.ToString("y"));
            IEnumerable<Meeting> meetingList = mCon.GetByMonth(e);
            rptMeetings.DataSource = meetingList;
            rptMeetings.DataBind();
            this.CurrentMeetingList = meetingList;


        }

        protected void rptMeetings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            //Console.WriteLine(sender.ToString());
        }

        protected void calSelect_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            lblAgendaTitle.Text = String.Format("{0}", e.NewDate.ToString("y"));
            IEnumerable<Meeting> meetingList = mCon.GetByMonth(e.NewDate);
            rptMeetings.DataSource = meetingList;
            rptMeetings.DataBind();
            this.CurrentMeetingList = meetingList;


            HighlightDates(meetingList);
        }

        private void HighlightDates(IEnumerable<Meeting> list)
        {
            foreach (Meeting m in list)
            {
                calMDMMeetings.SelectedDates.Add(m.Begining);
            }
        }
    }
}