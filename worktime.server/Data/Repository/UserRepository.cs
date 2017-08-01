using System;
using worktime.server.Data.DataStore;
using worktime.server.Data.Model;

namespace worktime.server.Data.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly IUserDataStore _store;
    public UserRepository(IUserDataStore store){
      _store = store;
    }
    void IUserRepository.CreateOrUpdate(Model.User user)
    {
      var dbUser = _store.Get(user.Id);
      if(dbUser == null){
        _store.Add(user);
      }else{
        _store.Update(user);
      }
    }
  }
}