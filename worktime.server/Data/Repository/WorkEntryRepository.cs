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
      var dbEntry = _repro.GetWorkEntry(userid, entry.Id);
      if(dbEntry == null){
          _repro.Add(userid, entry);
      }
      else{
          _repro.Update(userid, entry);
      }
    }

    public List<WorkEntry> GetWorkEntrys(string userid)
    {
      return _repro.GetWorkEntrys(userid);
    }
  }
}