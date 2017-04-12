using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForsythCo.Modules.MeetingDocumentManager.Components;
namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class EditDocumentTypes : MeetingDocumentManagerModuleBase
    {
        DocumentGroupController docCon = new DocumentGroupController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserInfo.IsInRole("MeetingAdmin"))
                    Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to create document types.  Please contact your System Administrator.");

                LoadList();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void LoadList()
        {
            lstDocGroups.DataSource = docCon.GetDocumentGroups();
            lstDocGroups.DataBind();
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            docCon.CreateDocumentGroup(new DocumentGroup()
            {
                GroupName = txtGroupName.Text,
                ShortDescription = txtDescription.Text,
                SortOrder = 0
            });

            txtDescription.Text = "";
            txtGroupName.Text = "";
            LoadList();
        }
    }
}