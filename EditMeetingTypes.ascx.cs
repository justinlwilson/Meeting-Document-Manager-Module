using System;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Users;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Services.Exceptions;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class EditMeetingTypes : MeetingDocumentManagerModuleBase
    {
        private MeetingTypeController mtCon = new MeetingTypeController();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!UserInfo.IsInRole("MeetingAdmin"))
                        Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to edit Meeting Types.  Please contact your System Administrator.");


                    UpdateDataGrid();
                }
            }
            catch(Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        protected void btnAddType_Click(object sender, EventArgs e)
        {
            new MeetingTypeController().CreateMeetingType(new MeetingType() { 
                TypeName = txtTypeName.Text,
                ShortDescription = txtDescription.Text
            });
            UpdateDataGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(DotNetNuke.Common.Globals.NavigateURL());
        }

        protected void dgMeetingTypes_EditCommand(object source, DataGridCommandEventArgs e)
        {
            hdnItemID.Value = e.Item.Cells[0].Text;
            txtTypeName.Text = e.Item.Cells[1].Text;
            txtDescription.Text = e.Item.Cells[2].Text;
            btnAddType.Visible = false;
            btnUpdate.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            new MeetingTypeController().UpdateMeetingType(new MeetingType()
            {
                MeetingTypeID = Convert.ToInt32(hdnItemID.Value),
                TypeName = txtTypeName.Text,
                ShortDescription = txtDescription.Text
            });
            ClearForm();
            UpdateDataGrid();
        }

        protected void ClearForm()
        {
            txtDescription.Text = "";
            txtTypeName.Text = "";
            hdnItemID.Value = "";
            btnUpdate.Visible = false;
            btnAddType.Visible = true;
        }

        protected void UpdateDataGrid()
        {
            dgMeetingTypes.DataSource = mtCon.GetMeetingTypes();
            dgMeetingTypes.DataBind();
        }
    }
}