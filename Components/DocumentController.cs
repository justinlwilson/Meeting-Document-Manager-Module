using System.Collections.Generic;
using DotNetNuke.Data;
using System.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    public class DocumentController
    {
        private string obj = SqlDataProvider.Instance().ObjectQualifier;

        public IEnumerable<Document> GetDocuments(int MeetingID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<Document>(CommandType.Text, string.Format("SELECT * FROM {0}MDM_Documents WHERE MeetingID = {1} ORDER BY DocumentGroupID", obj, MeetingID));
            }
        }

        public Document GetDocumentByID(int DocumentID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteSingleOrDefault<Document>(CommandType.Text, "WHERE DocumentID = @0", DocumentID);
            }
        }

        public void UpdateDocument(Document doc)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<Document>().Update(doc);
            }
        }

        public void AddDocument(Document doc)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<Document>().Insert(doc);
            }
        }
    }
}