using System;
using worktime.server.Data.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace worktime.server.Data.DataStore.Mongo
{
  public class UserDataStore : IUserDataStore
  {
    public void Add(User user)
    {
      var dbuser = Mongo.Model.User.FromDataModel(user);
      new MongoDb()
        .Get()
        .GetCollection<Mongo.Model.User>(MongoSettings.TableUsers)
        .InsertOne(dbuser);
    }

    public User Get(string id)
    {
      var user = new MongoDb()
        .Get()
        .GetCollection<Mongo.Model.User>(MongoSettings.TableUsers)
        .Find(u => u.UserId == id)
        .FirstOrDefault();
      if (user != null)
      {
        return user.ToDataModel();
      }
      return null;
    }

    public void Update(User user)
    {
      var filter = Builders<Mongo.Model.User>.Filter.Eq("UserId", user.Id);
      var update = Builders<Mongo.Model.User>.Update.Set("", "");
      var options = new MongoDB.Driver.FindOneAndUpdateOptions<Mongo.Model.User>();

      var updated = new MongoDb()
        .Get()
        .GetCollection<Mongo.Model.User>(MongoSettings.TableUsers)
        .FindOneAndUpdate(filter, update, options);
    }
  }
}