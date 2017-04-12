using System.Collections.Generic;
using DotNetNuke.Data;
using System.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    public class MarkerController
    {
        private string obj = SqlDataProvider.Instance().ObjectQualifier;

        public void AddMarker(Marker m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<Marker>().Insert(m);
            }
        }

        public void DeleteMarker(Marker m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<Marker>().Delete(m);
            }
        }

        public IEnumerable<Marker> GetMarkers(Meeting m)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Marker>(CommandType.Text, "WHERE MeetingID = @0 ORDER BY KeyTime", m.MeetingID);
        }

        public IEnumerable<Marker> GetMarkers(int m)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Marker>(CommandType.Text, "WHERE MeetingID = @0 ORDER BY KeyTime", m);
        }

        public Marker GetMarkerByMarkerID(int id)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteSingleOrDefault<Marker>(CommandType.Text, "WHERE MarkerID = @0", id);
        }

        public IEnumerable<Marker> GetAllMarkers()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.GetRepository<Marker>().Get();
        }
    }
}