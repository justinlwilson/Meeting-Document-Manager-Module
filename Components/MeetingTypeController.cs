using System.Collections.Generic;
using DotNetNuke.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    public class MeetingTypeController
    {
        public void CreateMeetingType(MeetingType t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<MeetingType>();
                rep.Insert(t);
            }
        }

        public IEnumerable<MeetingType> GetMeetingTypes()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<MeetingType>(System.Data.CommandType.Text, "ORDER BY TypeName");
        }

        public void UpdateMeetingType(MeetingType m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<MeetingType>().Update(m);
            }
        }

        public MeetingType GetMeetingTypeByID(int ID)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.GetRepository<MeetingType>().GetById(ID);
            }
        }
    }
}