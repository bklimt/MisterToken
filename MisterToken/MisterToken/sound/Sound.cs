using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MisterToken {
    public class Sound {
        private Sound() {
        }

        public static void LoadContent(ContentManager content) {
            manager.LoadContent(content);
        }

        public static void Play(SoundHook sound) {
            manager.Play(sound);
        }

        private static SoundManager manager = new SoundManager();
    }
}
