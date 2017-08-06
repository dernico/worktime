using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore
{
  public class WorkEntryMongoDataStore : IWorkEntryDataStore
  {

    private readonly MongoClient _client;
    private readonly IMongoDatabase _db;


    public WorkEntryMongoDataStore()
    {
        var username = MongoSettings.Username;
        var password = MongoSettings.Password;
        _client = new MongoClient($"mongodb://{username}:{password}@ds011913.mlab.com:11913/niconotes");
        _db = _client.GetDatabase(MongoSettings.Database);
    }

    public void Add(string userid, WorkEntry entry)
    {
        var mongoEntry = MongoModel.WorkEntry.FromDataModel(userid, entry);
        _db.GetCollection<MongoModel.WorkEntry>(MongoSettings.TableWorkEntrys).InsertOne(mongoEntry);
    }
    public void Update(string userid, WorkEntry entry)
    {
        AddOrUpdate(userid, entry);
    }
    private void AddOrUpdate(string userid, WorkEntry entry)
    {

    }

    public List<WorkEntry> GetWorkEntrys(string userid)
    {
        return _db
            .GetCollection<MongoModel.WorkEntry>(MongoSettings.TableWorkEntrys)
            .Find(w => w.UserId == userid)
            .ToList()
            .Select(w => w.ToDataModel())
            .ToList();
    }
    public WorkEntry GetWorkEntry(string userid, string entryid)
    {
        if(string.IsNullOrEmpty(entryid)) return null;

        var result = _db
            .GetCollection<MongoModel.WorkEntry>(MongoSettings.TableWorkEntrys)
            .Find(w => w.UserId == userid && w.EntryId == entryid)
            .FirstOrDefault();
        if(result != null)
        {
            return result.ToDataModel();
        }
        return null;
    }
  }
}