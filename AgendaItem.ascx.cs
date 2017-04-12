using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class AgendaItem : ForsythUserControlBase
    {
        public string MeetingGroup { 
            set { lnkMeetingGroup.Text = value; }
            get { return lnkMeetingGroup.Text; }
        }
        public string MeetingType {
            get { return lblMeetingType.Text; }
            set { lblMeetingType.Text = value; }
        }

        public string MeetingDay { 
            get { return lblMeetingDay.Text; }
            set { lblMeetingDay.Text = value; }
        }

        public string MeetingMonth { 
            get { return lblMeetingMonth.Text; }
            set { lblMeetingMonth.Text = value; }
        }

        public string MeetingTime { 
            get { return lblMeetingTime.Text; } 
            set { lblMeetingTime.Text = value; }
        }

        public string MeetingID 
        {  
            set
            {
                lnkMeetingGroup.NavigateUrl = String.Format("{0}/Meeting/{1}", this.BaseURL, value);
            } 
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        public void LoadInfo()
        {
            
        }
    }
}