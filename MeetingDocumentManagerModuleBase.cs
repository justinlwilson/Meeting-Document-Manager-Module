using System;
using DotNetNuke.Entities.Modules;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public class MeetingDocumentManagerModuleBase : PortalModuleBase
    {
        public int ItemId
        {
            get
            {
                var qs = Request.QueryString["tid"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }

        }

        public int DocumentID
        {
            get
            {
                var qs = Request.QueryString["did"];
                if (qs != null)
                    return Convert.ToInt32(qs);
                return -1;
            }

        }

        public int Meeting
        {
            get
            {
                var m = Request.QueryString["Meeting"];
                if (m != null)
                    return Convert.ToInt32(m);
                return -1;
            }
        }

        public int Group
        {
            get
            {
                var m = Request.QueryString["Group"];
                if (m != null)
                    return Convert.ToInt32(m);
                return -1;
            }
        }
    }
}