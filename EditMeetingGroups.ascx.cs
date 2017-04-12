using System;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Users;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Services.Exceptions;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class EditMeetingGroups : MeetingDocumentManagerModuleBase
    {
        private MeetingGroupController mgCon = new MeetingGroupController();
        protected void Page_Load(object sender, EventArgs e)
        {
            try 
            {
                if (!Page.IsPostBack)
                {
                    if (!UserInfo.IsInRole("MeetingAdmin"))
                        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to edit Meeting Groups.  Please contact your System Administrator.");


                    UpdateDataGrid();
                }
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            mgCon.AddMeetingGroup(new MeetingGroup()
            { 
                 GroupName = txtGroupName.Text,
                 ShortDescription = txtDescription.Text
            });
            ClearForm();
            UpdateDataGrid();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            mgCon.UpdateMeetingGroup(new MeetingGroup()
            {
                MeetingGroupID = Convert.ToInt32(hdnGroupID.Value),
                GroupName = txtGroupName.Text,
                ShortDescription = txtDescription.Text
            });
            ClearForm();
            UpdateDataGrid();
        }

        protected void ClearForm()
        {
            txtDescription.Text = "";
            txtGroupName.Text = "";
            hdnGroupID.Value = "";
            btnAddGroup.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void UpdateDataGrid()
        {
            dgMeetingGroups.DataSource = mgCon.GetMeetingGroups();
            dgMeetingGroups.DataBind();
        }

        protected void dgMeetingGroups_EditCommand(object source, DataGridCommandEventArgs e)
        {
            hdnGroupID.Value = e.Item.Cells[0].Text;
            txtGroupName.Text = e.Item.Cells[1].Text;
            txtDescription.Text = e.Item.Cells[2].Text;
            btnAddGroup.Visible = false;
            btnUpdate.Visible = true;
        }
    }
}