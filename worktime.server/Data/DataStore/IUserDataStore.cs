namespace worktime.server.Data.DataStore
{
    public interface IUserDataStore
    {
         Model.User Get(string id);
         void Add(Model.User user);
         void Update(Model.User user);
    }
}