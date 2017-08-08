using System.Collections.Generic;
using worktime.server.Data.Model;

namespace worktime.server.Data.Repository
{
    public interface IWorkEntryRepository
    {
        List<WorkEntry> GetWorkEntrys(string userid);
        void AddOrUpdateEntry(string userid, WorkEntry entry);
        void Delete(string userid, string entryid);
    }
}