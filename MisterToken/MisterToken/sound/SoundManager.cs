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
            /*map[SoundHook.CLEAR] = content.Load<SoundEffect>("mrowp");
            map[SoundHook.DUMP] = content.Load<SoundEffect>("mwerp");
            map[SoundHook.LOST] = content.Load<SoundEffect>("mwerp");
            map[SoundHook.ROTATE_LEFT] = content.Load<SoundEffect>("rotate2");
            map[SoundHook.ROTATE_RIGHT] = content.Load<SoundEffect>("rotate1");
            map[SoundHook.SLAM] = content.Load<SoundEffect>("click2");
            map[SoundHook.WON] = content.Load<SoundEffect>("mrowp");*/
        }

        public void Play(SoundHook sound) {
            if (map.ContainsKey(sound)) {
                map[sound].Play();
            }
        }

        private Dictionary<SoundHook, SoundEffect> map;
    }
}
