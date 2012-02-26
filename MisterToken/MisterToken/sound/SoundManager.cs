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
            map[SoundHook.CLEAR] = content.Load<SoundEffect>("chord");
            map[SoundHook.DUMP] = content.Load<SoundEffect>("chord");
            map[SoundHook.LOST] = content.Load<SoundEffect>("chord");
            map[SoundHook.ROTATE_LEFT] = content.Load<SoundEffect>("chord");
            map[SoundHook.ROTATE_RIGHT] = content.Load<SoundEffect>("chord");
            map[SoundHook.SLAM] = content.Load<SoundEffect>("chord");
            map[SoundHook.WON] = content.Load<SoundEffect>("chord");
        }

        public void Play(SoundHook sound) {
            if (map.ContainsKey(sound)) {
                map[sound].Play();
            }
        }

        private Dictionary<SoundHook, SoundEffect> map;
    }
}
