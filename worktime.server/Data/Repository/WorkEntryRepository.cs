using System;
using System.Collections.Generic;
using worktime.server.Data.DataStore;
using worktime.server.Data.Model;

namespace worktime.server.Data.Repository
{
  public class WorkEntryRepository : IWorkEntryRepository
  {
    private readonly IWorkEntryDataStore _repro;

    public WorkEntryRepository(IWorkEntryDataStore repro)
    {
      _repro = repro;
    }

    public void AddOrUpdateEntry(string userid, WorkEntry entry)
    {
      if (string.IsNullOrEmpty(entry.Id))
      {
        entry.Id = Guid.NewGuid().ToString();
        _repro.Add(userid, entry);
      }
      else
      {
        _repro.Update(userid, entry);
      }
    }

    public void Delete(string userid, string entryid)
    {
      _repro.Delete(userid, entryid);
    }

    public List<WorkEntry> GetWorkEntrys(string userid)
    {
      return _repro.GetWorkEntrys(userid);
    }
  }
}