namespace worktime.server.Data.Repository
{
    public interface IUserRepository
    {
         void CreateOrUpdate(Model.User user);
    }
}