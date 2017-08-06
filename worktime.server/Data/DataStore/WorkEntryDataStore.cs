using System;
using System.Collections.Generic;
using System.Linq;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore
{
  public class WorkEntryDataStore : IWorkEntryDataStore
  {
    public void Add(string userid, WorkEntry entry)
    {
        AddOrUpdate(userid, entry);
    }
    public void Update(string userid, WorkEntry entry)
    {
        AddOrUpdate(userid, entry);
    }
    private void AddOrUpdate(string userid, WorkEntry entry){

        var dict = new FileDataStoreHelper()
            .Load<Dictionary<string, List<WorkEntry>>>();

        if(dict == null){
            dict = new Dictionary<string, List<WorkEntry>>();
        }

        List<WorkEntry> entrys;
        if(!dict.ContainsKey(userid)){
            entrys = new List<WorkEntry>();
        }else{
            entrys = dict[userid];
        }

        if(string.IsNullOrEmpty(entry.Id)){
            entry.Id = Guid.NewGuid().ToString();
            entrys.Add(entry);
        }else{
            entrys.Remove(entrys.FirstOrDefault(e => e.Id == entry.Id));
            entrys.Add(entry);
        }
        new FileDataStoreHelper().Save(dict);
    }

    public List<WorkEntry> GetWorkEntrys(string userid)
    {
        var dict = new FileDataStoreHelper()
            .Load<Dictionary<string, List<WorkEntry>>>();
        
        if(dict == null){
            dict = new Dictionary<string, List<WorkEntry>>();
        }

        if(dict.ContainsKey(userid)){
            return dict[userid];
        }

        return new List<WorkEntry>();
    }
    public WorkEntry GetWorkEntry(string userid, string entryid)
    {
        if(string.IsNullOrEmpty(entryid)) return null;

        var dict = new FileDataStoreHelper()
            .Load<Dictionary<string, List<WorkEntry>>>();
        
        if(dict == null){
            dict = new Dictionary<string, List<WorkEntry>>();
        }

        if(dict.ContainsKey(userid)){
            return dict[userid].FirstOrDefault(e => e.Id == entryid);
        }

        return null;
    }
  }
}