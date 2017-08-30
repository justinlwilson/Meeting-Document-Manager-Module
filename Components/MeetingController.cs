using System.Collections.Generic;
using DotNetNuke.Data;
using System.Data;
using System;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    public class MeetingController
    {
        private string obj = SqlDataProvider.Instance().ObjectQualifier;

        public void SP_CreateMeeting(MeetingSP o)
        {
            o.ErrMsg = "";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(CommandType.StoredProcedure, string.Format("{0}MDM_Meeting_Create", obj), o.MeetingGroupID, o.LocationID, 
                    o.MeetingTypeID, o.VimeoNumber, o.Flag, o.Begining, o.Published, o.UserID, o.SPStatus, o.ErrMsg);
            }
            if (o.ErrMsg != "")
                throw new System.Exception(o.ErrMsg);
        }



        public void SP_UpdateMeeting(MeetingSP o)
        {
            o.ErrMsg = "";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(CommandType.StoredProcedure, string.Format("{0}MDM_Meeting_Update", obj), o.MeetingID, o.MeetingGroupID, o.LocationID, o.MeetingTypeID,
                    o.VimeoNumber, o.Flag, o.Begining, o.Published, o.UserID, o.SPStatus, o.ErrMsg);
            }
            if (o.ErrMsg != "")
                throw new System.Exception(o.ErrMsg);
        }

        public void UpdateMeeting(Meeting m, int userid)
        {
            string err = "";
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.Execute(CommandType.StoredProcedure, string.Format("{0}MDM_Meeting_Update", obj), m.MeetingID, m.MeetingGroupID, m.LocationID, m.MeetingTypeID,
                    m.VimeoNumber, m.Flag, m.Begining, m.Published, userid, 0, err);
            }
            if (err != "")
                throw new Exception(err);
        }

        public IEnumerable<Meeting> GetFilteredByGT(int group, int type)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<Meeting>(CommandType.StoredProcedure, string.Format("{0}MDM_MeetingsFilterGT", obj), group, type);
            }
        }

        public IEnumerable<Meeting> GetFilteredByDate(int month, int year)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                return ctx.ExecuteQuery<Meeting>(CommandType.StoredProcedure, string.Format("{0}MDM_MeetingsFilterDate", obj), month, year);
            }
        }

        public void UpdateTheMeeting(Meeting m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<Meeting>().Update(m);
            }
        }

        public IEnumerable<Meeting> GetAllActiveMeetings()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "WHERE Flag IS NULL ORDER BY Begining DESC");
        }

        public IEnumerable<Meeting> GetAllMeetings()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "ORDER BY Begining DESC");
        }

        public IEnumerable<Meeting> GetByOffset(int offset, int pagesize)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "ORDER BY Begining DESC OFFSET @0 ROW FETCH NEXT @1 ROWS ONLY", offset, pagesize);
        }

        public Meeting GetMeetingByID(int MeetingID)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteSingleOrDefault<Meeting>(CommandType.Text, "WHERE MeetingID = @0", MeetingID);
        }

        public IEnumerable<Meeting> GetMeetingByLocation(int LocationID)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "WHERE LocationID = @0", LocationID);
        }

        public IEnumerable<Meeting> GetMeetingByLocation(Location l)
        {
            return this.GetMeetingByLocation(l.LocationID);
        }

        public IEnumerable<Meeting> GetByMeetingGroup(MeetingGroup group)
        {
            return this.GetByMeetingGroup(group.MeetingGroupID);
        }

        public IEnumerable<Meeting> GetByMeetingGroup(int MeetingGroupID)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "WHERE MeetingGroupID = @0", MeetingGroupID);
        }

        public IEnumerable<Meeting> GetUpcomingMeetings()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "WHERE Begining >= GETDATE() ORDER BY Begining");
        }

        public IEnumerable<Meeting> GetByDate(DateTime date)
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "WHERE CONVERT(DATETIME, FLOOR(CONVERT(FLOAT, Begining))) = @0", date.ToShortDateString());
        }

        public IEnumerable<Meeting> GetByMonth(DateTime FirstOfMonth)
        {
            using(IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<Meeting>(CommandType.Text, "WHERE Begining >= @0 AND Begining < @1 ORDER BY Begining", FirstOfMonth.ToShortDateString(), FirstOfMonth.AddMonths(1).ToShortDateString());
        }

    }
}