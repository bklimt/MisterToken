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
            map[SoundHook.PLAYER_ONE_CLEAR] = content.Load<SoundEffect>("chord");
            map[SoundHook.PLAYER_TWO_CLEAR] = content.Load<SoundEffect>("chord");
            map[SoundHook.PLAYER_ONE_WON] = content.Load<SoundEffect>("chord");
            map[SoundHook.PLAYER_TWO_WON] = content.Load<SoundEffect>("chord");
            map[SoundHook.PLAYER_ONE_LOST] = content.Load<SoundEffect>("chord");
            map[SoundHook.PLAYER_TWO_LOST] = content.Load<SoundEffect>("chord");
        }

        public void Play(SoundHook sound) {
            map[sound].Play();
        }

        private Dictionary<SoundHook, SoundEffect> map;
    }
}
