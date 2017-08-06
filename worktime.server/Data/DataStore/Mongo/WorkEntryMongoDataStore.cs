using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore.Mongo
{
  public class WorkEntryMongoDataStore : IWorkEntryDataStore
  {
    public void Add(string userid, WorkEntry entry)
    {
      var mongoEntry = Model.WorkEntry.FromDataModel(userid, entry);
      new MongoDb().Get().GetCollection<Model.WorkEntry>(MongoSettings.TableWorkEntrys).InsertOne(mongoEntry);
    }

    public void Update(string userid, WorkEntry entry)
    {
      //AddOrUpdate(userid, entry);
    }
    private void AddOrUpdate(string userid, WorkEntry entry)
    {

    }

    public List<WorkEntry> GetWorkEntrys(string userid)
    {
      return new MongoDb()
          .Get()
          .GetCollection<Model.WorkEntry>(MongoSettings.TableWorkEntrys)
          .Find(w => w.UserId == userid)
          .ToList()
          .Select(w => w.ToDataModel())
          .ToList();
    }
    
    public WorkEntry GetWorkEntry(string userid, string entryid)
    {
      if (string.IsNullOrEmpty(entryid)) return null;

      var result = new MongoDb()
          .Get()
          .GetCollection<Model.WorkEntry>(MongoSettings.TableWorkEntrys)
          .Find(w => w.UserId == userid && w.EntryId == entryid)
          .FirstOrDefault();
      if (result != null)
      {
        return result.ToDataModel();
      }
      return null;
    }
  }
}