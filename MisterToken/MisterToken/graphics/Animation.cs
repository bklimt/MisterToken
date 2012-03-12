using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Animation : Drawable {
        public Animation(int millisPerFrame) {
            this.millisPerFrame = millisPerFrame;
            this.timeToNextFrame = millisPerFrame;
            this.current = 0;
            this.frame = new List<Texture2D>();
        }

        public void AddFrame(Texture2D texture) {
            frame.Add(texture);
        }

        public void Update(GameTime gameTime) {
            timeToNextFrame -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeToNextFrame <= 0) {
                current = (current + 1) % frame.Count;
                timeToNextFrame = millisPerFrame;
            }
        }

        public Texture2D GetTexture() {
            return frame[current];
        }

        private int millisPerFrame;
        private int timeToNextFrame;
        private int current;
        private List<Texture2D> frame;
    }
}
