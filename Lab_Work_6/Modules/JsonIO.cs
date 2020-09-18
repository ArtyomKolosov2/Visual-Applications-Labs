using Lab_Work_6.Modules;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace MyContacts.Modules
{
    public static class JsonIOservice
    {
        public static void WriteToJsonFile<T>(ObservableCollectionModifed<T> contacts, string path) where T : INotifyPropertyChanged
        {
            using (TextWriter writer = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, contacts);
            }
        }
        public static async Task WriteToJsonFileAsync<T>(ObservableCollectionModifed<T> contacts, string path) where T : INotifyPropertyChanged
        {
            using (TextWriter writer = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                await Task.Run(() => serializer.Serialize(writer, contacts));
            }
        }

        public static async Task<ObservableCollectionModifed<Contact>> LoadContactsAsync(string path)
        {
            return await Task.Run(() => LoadContacts(path));
        }
        public static ObservableCollectionModifed<Contact> LoadContacts(string path)
        {
            bool IsExist = File.Exists(path);
            ObservableCollectionModifed<Contact> readContacts = null;
            if (IsExist)
            {
                using (TextReader reader = File.OpenText(path))
                {
                    readContacts = (ObservableCollectionModifed<Contact>)JsonConvert.DeserializeObject(reader.ReadToEnd(), typeof(ObservableCollectionModifed<Contact>));
                }
            }
            else
            {
                File.CreateText(path);
            }
            return readContacts ?? new ObservableCollectionModifed<Contact>();
        }
    }
}
