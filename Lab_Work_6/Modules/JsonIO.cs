using System.IO;
using System.Text.Json;

namespace MyContacts.Modules
{
    public class JsonIOservice
    { 
        public string JsonPath { get; set; } = Directory.GetCurrentDirectory() + "/ContactsInfo.json";
        public void WriteToJsonFile(ObservableCollectionModifed<Contact> contacts)
        {
            using (StreamWriter writer = File.CreateText(JsonPath))
            {
                string jsonString = JsonSerializer.Serialize(contacts, contacts.GetType());
                writer.Write(jsonString);
            }
        }

        public ObservableCollectionModifed<Contact> LoadContacts()
        {
            var IsExist = File.Exists(JsonPath);
            ObservableCollectionModifed<Contact> readContacts = new ObservableCollectionModifed<Contact>();
            if (IsExist)
            {
                using (StreamReader reader = File.OpenText(JsonPath))
                {
                    string jsonString = reader.ReadToEnd();
                    if (jsonString.Length > 0) 
                    {
                        readContacts = (ObservableCollectionModifed<Contact>)JsonSerializer.Deserialize(jsonString, readContacts.GetType());
                    }
                }
            }
            else
            {
                File.CreateText(JsonPath);
            }
            return readContacts;
        }
    }
}
