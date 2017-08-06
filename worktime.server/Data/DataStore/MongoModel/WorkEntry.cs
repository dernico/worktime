using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace worktime.server.Data.DataStore.MongoModel
{
    public class WorkEntry
    {
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public string UserId {get;set;}
        
        [BsonElement("EntryId")]
        public string EntryId {get;set;}

        [BsonElement("Title")]
        public string Title {get;set;}
        
        [BsonElement("Description")]
        public string Description {get;set;}
        
        [BsonElement("StartTime")]
        public DateTime StartTime {get;set;}
        
        [BsonElement("EndTime")]
        public DateTime EndTime {get;set;}


        public static WorkEntry FromDataModel(string userid, Data.Model.WorkEntry entry){
            var mongoEntry = new MongoModel.WorkEntry();
            mongoEntry.Description = entry.Description;
            mongoEntry.EndTime = entry.EndTime;
            mongoEntry.StartTime = entry.StartTime;
            mongoEntry.Title = entry.Title;
            mongoEntry.EntryId = entry.Id;
            mongoEntry.UserId = userid;
            return mongoEntry;
        }
        public Data.Model.WorkEntry ToDataModel(){
            var dataModel = new Data.Model.WorkEntry();
            dataModel.Description = this.Description;
            dataModel.EndTime = this.EndTime;
            dataModel.StartTime = this.StartTime;
            dataModel.Title = this.Title;
            dataModel.Id = this.EntryId;
            return dataModel;
        }
    }
}