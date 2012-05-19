using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public interface MenuItem {
        void Draw(Rectangle area, SpriteBatch spriteBatch);
        void Draw2(int x, int y, SpriteBatch spriteBatch);
        void OnEnter();
        bool IsEnabled();
    }
}
