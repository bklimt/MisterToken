using System;
using System.IO;
using System.Xml.Serialization;

namespace MisterToken {
    public class StorageManager {
        public StorageManager() {
            saveData = new SaveData();
        }

        private string SaveFilePath {
            get {
                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var misterToken = Path.Combine(appData, "MisterToken");
                if (!Directory.Exists(misterToken)) {
                    Directory.CreateDirectory(misterToken);
                }
                var path = Path.Combine(misterToken, "save.xml");
                return path;
            }
        }

        public void Load(StorageCallback callback) {
            if (!File.Exists(SaveFilePath)) {
                saveData = new SaveData();
                callback();
                return;
            }

            using (var reader = File.OpenText(SaveFilePath)) {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                saveData = (SaveData)serializer.Deserialize(reader);
                callback();
            }
        }

        public void Save() {
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            using (var writer = File.OpenWrite(SaveFilePath)) {
                serializer.Serialize(writer, saveData);
            }
        }

        public SaveData GetSaveData() {
            return saveData;
        }

        public delegate void StorageCallback();
        private SaveData saveData;
    }
}
