using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore.File
{
  public class FileUserDataStore : IUserDataStore
  {
    private const string _dataname = nameof(FileUserDataStore);
    private Dictionary<string, User> _users;
    public FileUserDataStore(){
      _users = new FileDataStoreHelper(_dataname).Load<Dictionary<string, User>>();
      if(_users == null){
        _users = new Dictionary<string, User>();
      }
    }

    void IUserDataStore.Add(User user)
    {
      _users.Add(user.Id, user);
      new FileDataStoreHelper(_dataname).Save(_users);
    }

    User IUserDataStore.Get(string id)
    {
      return _users
        .Where(u => u.Key == id)
        .Select(u => u.Value)
        .FirstOrDefault();
    }

    void IUserDataStore.Update(User user)
    {
      var dbuser = (this as IUserDataStore).Get(user.Id);
      if(dbuser != null){
        dbuser.DisplayName = user.DisplayName;
        dbuser.PictureUrl = user.PictureUrl;
        dbuser.FirstName = user.FirstName;
        dbuser.LastName = user.LastName;
      }
      new FileDataStoreHelper(_dataname).Save(_users);
    }
  }
}