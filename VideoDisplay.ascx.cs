using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class VideoDisplay : MeetingDocumentManagerModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadMarkers(IEnumerable<Marker> mList)
        {
            int c = 0;
            foreach(Marker m in mList)
            {
                c++;
            }

            if(c>0)
                lblBookmarks.Visible = true;
            rptMarkers.DataSource = mList;
            rptMarkers.DataBind();
        }

        public void LoadVimeo(string VimeoSrc)
        {
            List<VimeoObj> v = new List<VimeoObj>();

            v.Add(new VimeoObj() { VimeoSrc = VimeoSrc });
            vimeo.DataSource = v;
            vimeo.DataBind();
            
        }
    }

    class VimeoObj
    {
        public string VimeoSrc { get; set; }
    }
}