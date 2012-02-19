using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class SpriteManager {
        public SpriteManager() {
        }

        public void LoadContent(ContentManager content, GraphicsDevice device) {
            tokenTextures = new Texture2D[Cell.COLORS * 6 + 1];

            tokenTextures[0] = content.Load<Texture2D>("black");

            tokenTextures[1] = content.Load<Texture2D>("red");
            tokenTextures[2] = content.Load<Texture2D>("yellow");
            tokenTextures[3] = content.Load<Texture2D>("blue");

            tokenTextures[4] = content.Load<Texture2D>("red_top");
            tokenTextures[5] = content.Load<Texture2D>("yellow_top");
            tokenTextures[6] = content.Load<Texture2D>("blue_top");

            tokenTextures[7] = content.Load<Texture2D>("red_right");
            tokenTextures[8] = content.Load<Texture2D>("yellow_right");
            tokenTextures[9] = content.Load<Texture2D>("blue_right");

            tokenTextures[10] = content.Load<Texture2D>("red_bottom");
            tokenTextures[11] = content.Load<Texture2D>("yellow_bottom");
            tokenTextures[12] = content.Load<Texture2D>("blue_bottom");

            tokenTextures[13] = content.Load<Texture2D>("red_left");
            tokenTextures[14] = content.Load<Texture2D>("yellow_left");
            tokenTextures[15] = content.Load<Texture2D>("blue_left");

            tokenTextures[16] = content.Load<Texture2D>("red_locked");
            tokenTextures[17] = content.Load<Texture2D>("yellow_locked");
            tokenTextures[18] = content.Load<Texture2D>("blue_locked");
        }

        public Texture2D GetTextureForCell(Cell cell) {
            int index = 0;
            if (cell.color != Cell.Color.BLACK) {
                if (cell.locked) {
                    index += 15;
                } else {
                    switch (cell.direction) {
                        case Cell.Direction.UP: index += 3; break;
                        case Cell.Direction.RIGHT: index += 6; break;
                        case Cell.Direction.DOWN: index += 9; break;
                        case Cell.Direction.LEFT: index += 12; break;
                    }
                }
                switch (cell.color) {
                    case Cell.Color.RED: index += 1; break;
                    case Cell.Color.YELLOW: index += 2; break;
                    case Cell.Color.BLUE: index += 3; break;
                }
            }
            return tokenTextures[index];
        }

        Texture2D[] tokenTextures;
    }
}
