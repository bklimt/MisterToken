using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MisterToken {
    public class Menu {
        public Menu(Action back) {
            selected = 0;
            this.back = back;
            options = new List<Tuple<string, Action>>();
        }

        public void Add(String text, Action action) {
            options.Add(new Tuple<String, Action>(text, action));
        }

        public void Update() {
            if (Input.IsDown(BooleanInputHook.MENU_DOWN)) {
                selected = (selected + 1) % options.Count;
            }
            if (Input.IsDown(BooleanInputHook.MENU_UP)) {
                selected--;
                if (selected < 0) {
                    selected = options.Count - 1;
                }
            }
            if (Input.IsDown(BooleanInputHook.MENU_ENTER)) {
                options[selected].Item2();
            }
            if (Input.IsDown(BooleanInputHook.MENU_BACK)) {
                back();
            }
        }

        public void Draw(Rectangle rect, bool focused, SpriteBatch spriteBatch) {
            if (focused) {
                Sprites.DrawLayer(SpriteHook.SCREEN_50_LAYER, rect, spriteBatch);
            }
            int x = rect.Left;
            int y = rect.Top + 10;
            for (int i = 0; i < options.Count; ++i) {
                Sprites.DrawText(options[i].Item1, Color.Cyan, new Vector2(x + 55, y), spriteBatch);
                if (i == selected) {
                    Cell cell = new Cell();
                    cell.color = focused ? CellColor.ORANGE : CellColor.ORANGE;
                    Rectangle cellRect;
                    cellRect.X = x + 12;
                    cellRect.Y = y + 9;
                    cellRect.Width = Constants.CELL_SIZE;
                    cellRect.Height = Constants.CELL_SIZE;
                    Sprites.DrawCell(cell, cellRect, spriteBatch);
                }
                y += 60;
            }
            if (!focused) {
                Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, rect, spriteBatch);
            }
        }

        public delegate void Action();
        private Action back;
        private List<Tuple<String, Action>> options;
        private int selected;
    }
}