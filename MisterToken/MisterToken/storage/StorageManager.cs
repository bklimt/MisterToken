using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace MisterToken {
    public class StorageManager {
        public StorageManager() {
            saveData = new SaveData();
            device = null;
        }

        public void Load(StorageCallback callback) {
            CallWithStorageDevice(delegate() {
                device.BeginOpenContainer("MisterToken", delegate(IAsyncResult containerResult) {
                    StorageContainer container = device.EndOpenContainer(containerResult);
                    if (container.FileExists("save.xml")) {
                        Stream input = container.OpenFile("save.xml", FileMode.Open);
                        if (input != null) {
                            try {
                                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                                saveData = (SaveData)serializer.Deserialize(input);
                            } catch (Exception) {
                                saveData = new SaveData();
                            } finally {
                                input.Close();
                                callback();
                            }
                        } else {
                            saveData = new SaveData();
                            callback();
                        }
                    } else {
                        saveData = new SaveData();
                        callback();
                    }
                    container.Dispose();
                }, null);
            });
        }

        public void Save() {
            CallWithStorageDevice(delegate() {
                device.BeginOpenContainer("MisterToken", delegate(IAsyncResult containerResult) {
                    StorageContainer container = device.EndOpenContainer(containerResult);
                    if (container.FileExists("save.xml")) {
                        container.DeleteFile("save.xml");
                    }
                    Stream output = container.CreateFile("save.xml");
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                    serializer.Serialize(output, saveData);
                    output.Close();
                    container.Dispose();
                }, null);
            });
        }

        private void CallWithStorageDevice(StorageCallback callback) {
            if (device != null && device.IsConnected) {
                callback();
            } else {
                StorageDevice.BeginShowSelector(delegate(IAsyncResult deviceResult) {
                    device = StorageDevice.EndShowSelector(deviceResult);
                    if (device != null && device.IsConnected) {
                        callback();
                    }
                }, null);
            }
        }

        public SaveData GetSaveData() {
            return saveData;
        }

        public delegate void StorageCallback();
        private SaveData saveData;
        private StorageDevice device;
    }
}
