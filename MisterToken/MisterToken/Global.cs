
namespace MisterToken {
    class Global {
        private static InputManager input;
        public static InputManager Input {
            get {
                if (input == null) {
                    input = new InputManager();
                }
                return input;
            }
        }

        private static SpriteManager sprites;
        public static SpriteManager Sprites {
            get {
                if (sprites == null) {
                    sprites = new SpriteManager();
                }
                return sprites;
            }
        }

        private static SoundManager sound;
        public static SoundManager Sound {
            get {
                if (sound == null) {
                    sound = new SoundManager();
                }
                return sound;
            }
        }

        private static StorageManager storage;
        public static StorageManager Storage {
            get {
                if (storage == null) {
                    storage = new StorageManager();
                }
                return storage;
            }
        }

        private static LevelManager levels;
        public static LevelManager Levels {
            get {
                if (levels == null) {
                    levels = new LevelManager();
                }
                return levels;
            }
        }

    }
}
