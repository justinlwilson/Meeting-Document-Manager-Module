using System;
using DotNetNuke.Entities.Users;
using DotNetNuke.Common;
using ForsythCo.Modules.MeetingDocumentManager.Components;
using DotNetNuke.Services.Exceptions;

namespace ForsythCo.Modules.MeetingDocumentManager
{
    public partial class AddDocument : MeetingDocumentManagerModuleBase
    {
        private DocumentGroupController dgCon = new DocumentGroupController();
        private DocumentController docCon = new DocumentController();
        private int _meetingID = -1;
        private int _documentID = -1;
        private bool _isEdit = false;
        private Document _editDoc;
        private MeetingController _meetCon = new MeetingController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(UserInfo.IsInRole("MeetingEditors") || UserInfo.IsInRole("MeetingAdmin")))
                Response.Redirect(DotNetNuke.Common.Globals.NavigateURL() + "?error=You do not have permission to add documents to meetings.  Please contact your System Administrator.");

            if(!Page.IsPostBack)
            {
                PopulateDropDowns();
                
            }
            if (DocumentID != -1)
            {
                _isEdit = true;
                LoadMeeting();
            }
        }

        protected void LoadMeeting()
        {
            _editDoc = docCon.GetDocumentByID(DocumentID);

            if(!Page.IsPostBack)
                ddDocumentType.Items.FindByValue(_editDoc.DocumentGroupID.ToString()).Selected = true;

            txtContent.Text = _editDoc.Content;
        }

        protected void PopulateDropDowns()
        {
            if (ItemId == -1)
                ddDocumentType.DataSource = dgCon.GetDocumentGroups();
            else
                ddDocumentType.DataSource = dgCon.OutstandingDocumentsByMeeting(ItemId);
            ddDocumentType.DataBind();
        }



        protected void btnAddDocument_Click(object sender, EventArgs e)
        {
            if(_isEdit)
            {
                _editDoc = docCon.GetDocumentByID(DocumentID);
                _editDoc.Content = txtContent.Text;
                _editDoc.DocumentGroupID = Convert.ToInt32(ddDocumentType.SelectedValue);
                new DocumentController().UpdateDocument(_editDoc);

            }
            else 
            { 
                if (ItemId == -1)
                    throw new Exception("Meeting ID not selected.");

                new DocumentController().AddDocument(new Document() {
                    MeetingID = ItemId,
                    DocumentGroupID = Convert.ToInt32(ddDocumentType.SelectedValue),
                    Content = txtContent.Text,
                    UserID = UserId
                });

                Meeting m = _meetCon.GetMeetingByID(ItemId);
                m.UpdatedBy = UserId;
                m.RecordUpdated = DateTime.Now;
                _meetCon.UpdateTheMeeting(m);
            }
            Response.Redirect(Globals.NavigateURL(), true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(), true);
        }
    }
}