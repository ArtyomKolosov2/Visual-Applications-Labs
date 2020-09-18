using Lab_Work_6.Modules;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace MyContacts.Modules
{
    public class JsonIOservice
    {
        private JsonSerializer serializer = new JsonSerializer();
        public string JsonPath { get; set; } = Directory.GetCurrentDirectory() + "/ContactsInfo.json";
        public async Task WriteToJsonFileAsync<T>(ObservableCollectionModifed<T> contacts) where T : INotifyPropertyChanged
        {
            using (StreamWriter writer = File.CreateText(JsonPath))
            {
                await Task.Run(() => serializer.Serialize(writer, contacts));
            }
        }

        public async Task<ObservableCollectionModifed<Contact>> LoadContactsAsync()
        {
            return await Task.Run(() => LoadContacts());
        }
        public ObservableCollectionModifed<Contact> LoadContacts()
        {
            var IsExist = File.Exists(JsonPath);
            ObservableCollectionModifed<Contact> readContacts = null;
            if (IsExist)
            {
                using (TextReader reader = File.OpenText(JsonPath))
                {
                    readContacts = (ObservableCollectionModifed<Contact>)serializer.Deserialize(reader, typeof(ObservableCollectionModifed<Contact>));
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
