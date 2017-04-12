using System.Collections.Generic;
using DotNetNuke.Data;

namespace ForsythCo.Modules.MeetingDocumentManager.Components
{
    class AudioMIMEController
    {
        public void CreateMIME(AudioMIME m)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                ctx.GetRepository<AudioMIME>().Insert(m);
            }
        }

        public IEnumerable<AudioMIME> GetMIMETypes()
        {
            IEnumerable<AudioMIME> _mimeList;
            using (IDataContext ctx = DataContext.Instance())
            {
                _mimeList = ctx.GetRepository<AudioMIME>().Get();
            }
            return _mimeList;
        }
    }
}