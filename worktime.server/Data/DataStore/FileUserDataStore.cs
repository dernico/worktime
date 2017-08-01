using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using worktime.server.Data.Model;

namespace worktime.server.Data.DataStore
{
  public class FileUserDataStore : IUserDataStore
  {

    private const string FileName = "user.data";
    private const string Filepath = "/";
    private Dictionary<string, User> _users;
    public FileUserDataStore(){
      Load();
    }

    void IUserDataStore.Add(User user)
    {
      _users.Add(user.Id, user);
      Save();
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
      Save();
    }

    private void Load()
    {
      var filepath = Path.Combine(Filepath, FileName);
      if(!File.Exists(filepath)){
        var fs = File.Create(filepath);
        fs.Dispose();
      }
      var filecontent = File.ReadAllText(filepath);

      if (string.IsNullOrEmpty(filecontent))
      {
        _users = new Dictionary<string, User>();
        return;
      }

      _users = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, User>>(filecontent);
    }

    private void Save(){
      var filecontent = Newtonsoft.Json.JsonConvert.SerializeObject(_users);
      var filepath = Path.Combine(Filepath, FileName);
      File.WriteAllText(filepath, filecontent);
    }
  }
}