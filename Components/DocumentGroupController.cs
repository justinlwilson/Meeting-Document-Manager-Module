using System.Collections.Generic;
using DotNetNuke.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    class DocumentGroupController
    {
        private string obj = SqlDataProvider.Instance().ObjectQualifier;

        public IEnumerable<DocumentGroup> GetDocumentGroups()
        {
            IEnumerable<DocumentGroup> _documentGroups;
            using (IDataContext ctx = DataContext.Instance())
            {
                _documentGroups = ctx.ExecuteQuery<DocumentGroup>(System.Data.CommandType.Text, "ORDER BY SortOrder");
            }
            return _documentGroups;
        }

        public DocumentGroup GetDocumentGroupByID(int ID)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteSingleOrDefault<DocumentGroup>(System.Data.CommandType.Text, "WHERE DocumentGroupID = @0", ID);
        }

        public void CreateDocumentGroup(DocumentGroup docGroup)
        {
            using (IDataContext ctx = DataContext.Instance()) {
                ctx.GetRepository<DocumentGroup>().Insert(docGroup);
            }
        }

        public IEnumerable<DocumentGroup> OutstandingDocumentsByMeeting(int MeetingID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<DocumentGroup>(System.Data.CommandType.StoredProcedure, string.Format("{0}MDM_OutstandingDocumentsByMeeting", obj), MeetingID);
            }
        }
    }
}