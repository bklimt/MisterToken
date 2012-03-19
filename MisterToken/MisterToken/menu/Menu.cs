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
        public Menu(PlayerIndex player, Action back) {
            selected = 0;
            this.player = player;
            this.back = back;
            items = new List<MenuItem>();
        }

        public void Add(String text, DefaultMenuItem.MenuAction action) {
            items.Add(new DefaultMenuItem(text, action));
        }

        public void AddLevel(Level level, LevelMenuItem.MenuAction action) {
            items.Add(new LevelMenuItem(level, action));
        }

        public void Update() {
            if (Input.IsDown(PerPlayerBooleanInputHook.MENU_DOWN.ForPlayer(player)) ||
                Input.IsDown(PerPlayerAnalogInputHook.MENU_DOWN.ForPlayer(player))) {
                selected = (selected + 1) % items.Count;
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.MENU_UP.ForPlayer(player)) ||
                Input.IsDown(PerPlayerAnalogInputHook.MENU_UP.ForPlayer(player))) {
                selected--;
                if (selected < 0) {
                    selected = items.Count - 1;
                }
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.MENU_ENTER.ForPlayer(player)) ||
                Input.IsDown(PerPlayerBooleanInputHook.ROTATE_RIGHT.ForPlayer(player))) {
                items[selected].OnEnter();
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.MENU_BACK.ForPlayer(player)) ||
                Input.IsDown(PerPlayerBooleanInputHook.ROTATE_LEFT.ForPlayer(player))) {
                back();
            }
        }

        public void SetSelected(int selected) {
            this.selected = selected;
            // Clear the current keyboard state.
            Input.IsDown(PerPlayerBooleanInputHook.MENU_UP.ForPlayer(player));
            Input.IsDown(PerPlayerBooleanInputHook.MENU_DOWN.ForPlayer(player));
        }

        public void Draw(Rectangle rect, bool focused, SpriteBatch spriteBatch) {
            if (focused) {
                Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, rect, spriteBatch);
            }
            int x = rect.Left;
            int y = rect.Top + 10;
            for (int i = 0; i < items.Count; ++i) {
                MenuItem item = items[i];
                Rectangle itemRect;
                itemRect.X = x + 55;
                itemRect.Y = y;
                itemRect.Width = rect.Width - 55;
                itemRect.Height = 30;
                item.Draw(itemRect, spriteBatch);
                if (i == selected) {
                    Cell cell = new Cell();
                    cell.color = focused ? CellColor.ORANGE : CellColor.ORANGE;
                    Rectangle cellRect;
                    cellRect.X = x + 28;
                    cellRect.Y = y + 4;
                    cellRect.Width = (Constants.CELL_SIZE * 2) / 3;
                    cellRect.Height = (Constants.CELL_SIZE * 2) / 3;
                    Sprites.DrawCell(cell, cellRect, spriteBatch);
                }
                y += 30;
            }
            if (!focused) {
                Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, rect, spriteBatch);
            }
        }

        private PlayerIndex player;
        private Action back;
        private List<MenuItem> items;
        private int selected;
    }
}