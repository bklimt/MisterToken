using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MisterToken {
    public class TitleMenu {
        private bool singlePlayer;
        private TitleMenuListener listener;

        public TitleMenu(TitleMenuListener listener) {
            this.listener = listener;
            singlePlayer = true;
        }

        public void Update() {
            if (Input.IsDown(BooleanInputHook.MENU_DOWN) ||
                Input.IsDown(BooleanInputHook.MENU_UP)) {
                singlePlayer = !singlePlayer;
            }
            if (Input.IsDown(BooleanInputHook.PLAYER_ONE_START)) {
                if (singlePlayer) {
                    listener.OnStartSinglePlayer();
                } else {
                    listener.OnStartMultiPlayer();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
            Rectangle menu;
            menu.X = 455;
            menu.Y = 380;
            menu.Width = 320;
            menu.Height = 130;
            Sprites.DrawLayer(SpriteHook.SCREEN_50_LAYER, menu, spriteBatch);
            Sprites.Draw(SpriteHook.NUMBER_1, new Vector2(510, 400), spriteBatch);
            Sprites.Draw(SpriteHook.NUMBER_2, new Vector2(510, 450), spriteBatch);
            Sprites.Draw(SpriteHook.PLAYER, new Vector2(560, 400), spriteBatch);
            Sprites.Draw(SpriteHook.PLAYER, new Vector2(560, 450), spriteBatch);

            Cell cell = new Cell();
            cell.color = Cell.Color.ORANGE;
            Rectangle cellRect;
            cellRect.X = 467;
            cellRect.Y = singlePlayer ? 400 : 450;
            cellRect.Width = Constants.CELL_SIZE;
            cellRect.Height = Constants.CELL_SIZE;
            Sprites.DrawCell(cell, cellRect, spriteBatch);
        }
    }
}
