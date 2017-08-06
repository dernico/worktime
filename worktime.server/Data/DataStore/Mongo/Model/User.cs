using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace worktime.server.Data.DataStore.Mongo.Model
{
    public class User
    {
        
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public string UserId {get;set;}

        [BsonElement("DisplayName")]
        public string DisplayName {get;set;}

        [BsonElement("FirstName")]
        public string FirstName {get;set;}

        [BsonElement("LastName")]
        public string LastName {get;set;}

        [BsonElement("PictureUrl")]
        public string PictureUrl {get;set;}


        public Data.Model.User ToDataModel()
        {
            var user = new Data.Model.User();
            user.Id = this.UserId;
            user.DisplayName = this.DisplayName;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.PictureUrl = this.PictureUrl;
            return user;
        }

        public static User FromDataModel(Data.Model.User dataModel){
            var user = new User();
            user.UserId = dataModel.Id;
            user.DisplayName = dataModel.DisplayName;
            user.FirstName = dataModel.FirstName;
            user.LastName = dataModel.LastName;
            user.PictureUrl = dataModel.PictureUrl;
            return user;
        }
    }
}