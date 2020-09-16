using Lab_Work_6.Modules;
using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;


namespace MyContacts.Modules
{
    public class JsonIOservice
    {
        private JsonSerializer serializer = new JsonSerializer();
        public string JsonPath { get; set; } = Directory.GetCurrentDirectory() + "/ContactsInfo.json";
        public void WriteToJsonFile<T>(ObservableCollectionModifed<T> contacts) where T : INotifyPropertyChanged
        {
            using (StreamWriter writer = File.CreateText(JsonPath))
            {
                serializer.Serialize(writer, contacts);
            }
        }

        public ObservableCollectionModifed<Contact> LoadContacts()
        {
            var IsExist = File.Exists(JsonPath);
            ObservableCollectionModifed<Contact> readContacts = null;
            if (IsExist)
            {
                using (StreamReader reader = File.OpenText(JsonPath))
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
