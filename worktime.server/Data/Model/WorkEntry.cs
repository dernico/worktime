using System;

namespace worktime.server.Data.Model
{
    public class WorkEntry
    {
        public string Id {get;set;}
        public string Title {get;set;}
        public string Description {get;set;}
        public DateTime StartTime {get;set;}
        public DateTime EndTime {get;set;}
    }
}