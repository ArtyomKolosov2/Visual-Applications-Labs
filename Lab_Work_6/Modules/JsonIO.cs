using Lab_Work_6.Modules;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace MyContacts.Modules
{
    public class JsonIOservice
    {
        public string JsonPath { get; set; }

        public JsonIOservice(string path)
        {
            JsonPath = path;
        }
        public JsonIOservice() 
        {
            JsonPath = Directory.GetCurrentDirectory() + "/ContactsInfo.json";
        }
       
        public void WriteToJsonFile<T>(ObservableCollectionModifed<T> contacts) where T : INotifyPropertyChanged
        {
            using (TextWriter writer = File.CreateText(JsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, contacts);
            }
        }
        public async Task WriteToJsonFileAsync<T>(ObservableCollectionModifed<T> contacts) where T : INotifyPropertyChanged
        {
            using (TextWriter writer = File.CreateText(JsonPath))
            {
                JsonSerializer serializer = new JsonSerializer();
                await Task.Run(() => serializer.Serialize(writer, contacts));
            }
        }

        public async Task<ObservableCollectionModifed<Contact>> LoadContactsAsync()
        {
            return await Task.Run(() => LoadContacts());
        }
        public ObservableCollectionModifed<Contact> LoadContacts()
        {
            bool IsExist = File.Exists(JsonPath);
            ObservableCollectionModifed<Contact> readContacts = null;
            if (IsExist)
            {
                using (TextReader reader = File.OpenText(JsonPath))
                {
                    readContacts = (ObservableCollectionModifed<Contact>)JsonConvert.DeserializeObject(reader.ReadToEnd(), typeof(ObservableCollectionModifed<Contact>));
                }
            }
            else
            {
                File.CreateText(JsonPath);
            }
            return readContacts ?? new ObservableCollectionModifed<Contact>();
        }
    }
}
