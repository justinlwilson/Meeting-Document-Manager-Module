using System.Collections.Generic;
using DotNetNuke.Data;
using System.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    public class LocationController
    {
        private string obj = SqlDataProvider.Instance().ObjectQualifier;
        public void AddLocation(Location l)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(System.Data.CommandType.StoredProcedure, string.Format("{0}MDM_Location_Create", obj), l.BuildingName, l.AddressOne, l.AddressTwo, l.Latitude, l.Longitude, l.UserID, l.SPStatus, l.ErrMsg);
            }
        }

        public IEnumerable<Location> GetLocations()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<Location>(System.Data.CommandType.Text, string.Format("SELECT * FROM {0}MDM_vLocations ORDER BY BuildingName", obj));
            }
        }

        public Location GetLocation(int LocationID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteSingleOrDefault<Location>(CommandType.Text, string.Format("SELECT * FROM {0}MDM_vLocations WHERE LocationID = @0", obj), LocationID);
             //   return ctx.GetRepository<Location>().GetById(LocationID);
            }
        }

    }
}