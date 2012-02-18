using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public interface GameState {
        void LoadContent(ContentManager content, GraphicsDevice device);
        void Start();
        GameState Update(GameTime gameTime);
        void Draw(GraphicsDevice device, GameTime gameTime);
    }
}
