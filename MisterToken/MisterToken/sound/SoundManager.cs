using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MisterToken {
    public class SoundManager {
        public void LoadContent(ContentManager content) {
            map = new Dictionary<SoundHook, SoundEffect>();
            map[SoundHook.CLEAR_1] = content.Load<SoundEffect>("sounds/beam3");
            map[SoundHook.CLEAR_2] = content.Load<SoundEffect>("sounds/beam4");
            map[SoundHook.CLEAR_3] = content.Load<SoundEffect>("sounds/beam5");
        }

        public void Play(SoundHook sound) {
            if (map.ContainsKey(sound)) {
                map[sound].Play();
            }
        }

        private Dictionary<SoundHook, SoundEffect> map;
    }
}
