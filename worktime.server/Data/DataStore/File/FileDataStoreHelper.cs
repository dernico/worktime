using System.IO;

namespace worktime.server.Data.DataStore.File
{
  public class FileDataStoreHelper
  {
    private const string FILEPATH = ".";
    private const string FILENAME = "{0}.data";
    private readonly string _dataname;

    public FileDataStoreHelper(string name){
      _dataname = name;
    }

    private string GetFilePath<T>() => Path.Combine(FILEPATH, string.Format(FILENAME, _dataname));

    public T Load<T>() where T : class
    {
      var filepath = GetFilePath<T>();
      if (!System.IO.File.Exists(filepath))
      {
        var fs = System.IO.File.Create(filepath);
        fs.Dispose();
      }
      var filecontent = System.IO.File.ReadAllText(filepath);

      if (string.IsNullOrEmpty(filecontent))
      {
        return default(T);
      }

      return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(filecontent);
    }

    public void Save<T>(T tosave) where T : class
    {
      var filecontent = Newtonsoft.Json.JsonConvert.SerializeObject(tosave);
      var filepath = GetFilePath<T>();
      System.IO.File.WriteAllText(filepath, filecontent);
    }
  }
}