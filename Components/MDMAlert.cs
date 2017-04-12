using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using DotNetNuke.Data;
using System.Data;
using System.Collections.Generic;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [TableName("MDM_Alerts")]
    [PrimaryKey("AlertID", AutoIncrement=true)]
    [Cacheable("Alerts", CacheItemPriority.Default, 20)]
    public class MDMAlert
    {
        public int AlertID { get; set; }
        public string Caption { get; set; }
    }

    public class MDMAlertController
    {
        public void AddAlert(MDMAlert a)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<MDMAlert>().Insert(a);
            }
        }

        public IEnumerable<MDMAlert> GetAlerts()
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.GetRepository<MDMAlert>().Get();
            }
        }

        public void DeleteAlert(MDMAlert a)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<MDMAlert>().Delete(a);
            }
        }

        public void DeleteAlert(int id)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<MDMAlert>().Delete("WHERE AlertID = @0", id);
            }
        }
    }
}