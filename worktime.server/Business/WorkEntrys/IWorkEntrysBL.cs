using System.Collections.Generic;
using worktime.server.Data.Model;

namespace worktime.server.Business.WorkEntrys
{
    public interface IWorkEntrysBL
    {
         List<WorkEntry> GetWorkentrys(string userid);
         void SaveWorkEntry(string userid, WorkEntry entry);
         void DeleteWorkEntry(string userid, string entryid);
    }
}