using MongoDB.Driver;

namespace worktime.server.Data.DataStore.Mongo
{
    public class MongoDb
    {
        public IMongoDatabase Get(){
            var username = MongoSettings.Username;
            var password = MongoSettings.Password;
            var client = new MongoClient($"mongodb://{username}:{password}@ds011913.mlab.com:11913/niconotes");
            return client.GetDatabase(MongoSettings.Database);
        }
    }
}