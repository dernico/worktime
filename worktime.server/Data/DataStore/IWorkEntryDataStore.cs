using System.Collections.Generic;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore
{
    public interface IWorkEntryDataStore
    {
         List<WorkEntry> GetWorkEntrys(string userid);
         WorkEntry GetWorkEntry(string userid, string entryid);
         void Add(string userid, WorkEntry entry);
         void Update(string userid, WorkEntry entry);
         void Delete(string userid, string entryid);
    }
}