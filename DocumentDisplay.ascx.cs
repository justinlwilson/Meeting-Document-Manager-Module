using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class DocumentDisplay : MeetingDocumentManagerModuleBase
    {
        private bool _iseditable = false;

        public int ModuleID { get; set; }
        public bool IsEditable { get { return _iseditable; } set { _iseditable = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadDocuments(Meeting m)
        {
            if (m.Documents.Count() > 0) {
                rptDocuments.DataSource = m.Documents;
                rptDocuments.DataBind();
                Repeater1.DataSource = m.Documents;
                Repeater1.DataBind();
                //foreach (Document doc in m.Documents)
                //{
                 //   lblTest.Text += Server.HtmlDecode(doc.Content);
                //}
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var _lnkEdit = e.Item.FindControl("lnkEdit") as HyperLink;
                var d = (Document)e.Item.DataItem;
                if(IsEditable)
                {
                    _lnkEdit.Visible = true;
                    _lnkEdit.NavigateUrl = EditUrl(string.Empty, string.Empty, "Documents", "did=" + d.DocumentID, "mid=" + this.ModuleID);
                }
            }
        }
    }
}