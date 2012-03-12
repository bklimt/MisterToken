using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Image : Drawable {
        public Image(Texture2D image) {
            this.image = image;
        }

        public Texture2D GetTexture() {
            return image;
        }

        private Texture2D image;
    }
}
