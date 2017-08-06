using System;
using System.Collections.Generic;
using worktime.server.Data.Model;
using worktime.server.Data.Repository;

namespace worktime.server.Business.WorkEntrys
{
  public class WorkEntrysBL : IWorkEntrysBL
  {
    private readonly IWorkEntryRepository _repro;
    public WorkEntrysBL(IWorkEntryRepository repro)
    {
      _repro = repro;
    }
    public List<WorkEntry> GetWorkentrys(string userid)
    {
      return _repro.GetWorkEntrys(userid);
    }

    public void SaveWorkEntry(string userid, WorkEntry entry)
    {
      _repro.AddOrUpdateEntry(userid, entry);
    }
  }
}