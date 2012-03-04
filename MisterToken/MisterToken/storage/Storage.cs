using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class Storage {
        private Storage() {
        }

        public static void Save() {
            manager.Save();
        }

        public static void Load(StorageManager.StorageCallback callback) {
            manager.Load(callback);
        }

        public static SaveData GetSaveData() {
            return manager.GetSaveData();
        }

        private static StorageManager manager = new StorageManager();
    }
}
