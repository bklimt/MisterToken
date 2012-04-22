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
            soundMap = new Dictionary<SoundHook, SoundEffect>();
            soundMap[SoundHook.CLEAR_1] = content.Load<SoundEffect>("sounds/beam3");
            soundMap[SoundHook.CLEAR_2] = content.Load<SoundEffect>("sounds/beam4");
            soundMap[SoundHook.CLEAR_3] = content.Load<SoundEffect>("sounds/beam5");
            soundMap[SoundHook.SLAM] = content.Load<SoundEffect>("sounds/beep18");

            songMap = new Dictionary<MusicHook, Song>();
            songMap[MusicHook.SONG_1] = content.Load<Song>("music/MisterToken1");
            songMap[MusicHook.SONG_2] = content.Load<Song>("music/MisterToken2");
        }

        public void Play(SoundHook sound) {
            if (soundMap.ContainsKey(sound)) {
                soundMap[sound].Play();
            }
        }

        public void StartMusic(MusicHook song) {
            StopMusic();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(songMap[song]);
        }

        public void StopMusic() {
            MediaPlayer.Stop();
        }

        private Dictionary<SoundHook, SoundEffect> soundMap;
        private Dictionary<MusicHook, Song> songMap;
    }
}
