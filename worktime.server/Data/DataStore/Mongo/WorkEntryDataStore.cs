using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore.Mongo
{
  public class WorkEntryDataStore : IWorkEntryDataStore
  {
    public void Add(string userid, WorkEntry entry)
    {
      var mongoEntry = Model.WorkEntry.FromDataModel(userid, entry);
      new MongoDb().Get().GetCollection<Model.WorkEntry>(MongoSettings.TableWorkEntrys).InsertOne(mongoEntry);
    }

    public void Update(string userid, WorkEntry entry)
    {
      var filter = Builders<Mongo.Model.WorkEntry>.Filter.Eq(w => w.EntryId, entry.Id);
      var update = Builders<Mongo.Model.WorkEntry>.Update
        .Set(w=> w.Description, entry.Description)
        .Set(w => w.EndTime, entry.EndTime)
        .Set(w => w.EntryId, entry.Id)
        .Set(w => w.StartTime, entry.StartTime)
        .Set(w => w.Title, entry.Title)
        .Set(w => w.UserId, userid);
      var options = new MongoDB.Driver.FindOneAndUpdateOptions<Mongo.Model.WorkEntry>();

      new MongoDb()
        .Get()
        .GetCollection<Mongo.Model.WorkEntry>(MongoSettings.TableWorkEntrys)
        .FindOneAndUpdateAsync(filter, update, options);
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

    public void Delete(string userid, string entryid)
    {
      new MongoDb()
        .Get()
        .GetCollection<Model.WorkEntry>(MongoSettings.TableWorkEntrys)
        .FindOneAndDeleteAsync(w => w.UserId == userid && w.EntryId == entryid);
    }
  }
}