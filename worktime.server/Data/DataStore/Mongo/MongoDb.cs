using MongoDB.Driver;

namespace worktime.server.Data.DataStore.Mongo
{
    public class MongoDb
    {
        public IMongoDatabase Get(){
            var username = MongoSettings.Username;
            var password = MongoSettings.Password;
            var url = MongoSettings.Url;
            var db = MongoSettings.Database;
            var client = new MongoClient($"mongodb://{username}:{password}@{url}/{db}");
            return client.GetDatabase(MongoSettings.Database);
        }
    }
}