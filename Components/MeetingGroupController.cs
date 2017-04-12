using System.Collections.Generic;
using DotNetNuke.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    class MeetingGroupController
    {
        public void AddMeetingGroup(MeetingGroup mG)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<MeetingGroup>();
                rep.Insert(mG);
            }
        }
        public IEnumerable<MeetingGroup> GetMeetingGroups()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<MeetingGroup>(System.Data.CommandType.Text, "ORDER BY GroupName");

        }

        public void DeleteMeetingGroup(MeetingGroup item)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<MeetingGroup>();
                rep.Delete(item);
            }
        }

        public void UpdateMeetingGroup(MeetingGroup group)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<MeetingGroup>().Update(group);
            }
        }

        public MeetingGroup GetMeetingGroupByID(int id)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.GetRepository<MeetingGroup>().GetById(id);
            }
        }
    }
}