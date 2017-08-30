using System.Collections.Generic;
using Newtonsoft.Json;
using DotNetNuke.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MeetingYear
    {
        [JsonProperty(propertyName:"year")]
        public int Year { get; set; }
    }

    public class MeetingYearController
    {
        private string obj = SqlDataProvider.Instance().ObjectQualifier;

        public IEnumerable<MeetingYear> GetMeetingYears()
        {
            using (IDataContext ctx = DataContext.Instance())
                return ctx.ExecuteQuery<MeetingYear>(System.Data.CommandType.StoredProcedure, string.Format("{0}MDM_GetMeetingYears", obj));
        }
    }
}