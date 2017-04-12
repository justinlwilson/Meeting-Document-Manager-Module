using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class GoogleMap : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadMap(Location loc)
        {
            List<Location> locList = new List<Location>();
            locList.Add(loc);

            rptMap.DataSource = locList;
            rptMap.DataBind();
        }
    }
}