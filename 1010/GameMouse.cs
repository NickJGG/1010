﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _1010 {
    public class GameMouse : GameObject {
        Color color;
        
        float scale = 1f;

        public GameMouse(ContentManager c) {
            Enabled = true;
            CanPress = true;

            ImageNormal = c.Load<Texture2D>("mouse/mouse");
            ImageClick = c.Load<Texture2D>("mouse/mouseHover");

            int r = Utilities.Next(0, 255), g = Utilities.Next(r / 2, 255 - r / 2), b = Utilities.Next(g / 2, 255 - g / 2);

            //color = Color.FromNonPremultiplied(r, g, b, 255);
            color = Color.White;

            Image = ImageNormal;

            MouseState state = GetState();
            Position = Enabled ? state.Position.ToVector2() : Position;

            Size = new Point(1);
            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }

        public void Update() {
            MouseState state = GetState();

            CanPress = !LeftMouseDown && !RightMouseDown;
            CanType = Keyboard.GetState().GetPressedKeys().Length == 0;

            Position = Enabled ? new Vector2(state.Position.X + Camera.Position.X, state.Position.Y + Camera.Position.Y) : Position;
            Hitbox = new Rectangle(Position.ToPoint(), Size);

            LeftMouseDown = state.LeftButton == ButtonState.Pressed;
            RightMouseDown = state.RightButton == ButtonState.Pressed;

            Image = LeftMouseDown || RightMouseDown ? ImageClick : ImageNormal;
        }

        public void Draw(SpriteBatch sb) {
            if (DrawMouse)
                sb.Draw(Image, Position, null, color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }

        public MouseState GetState() {
            return Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public bool Hovering { get; set; }
        public bool Enabled { get; set; }
        public bool LeftMouseDown { get; set; }
        public bool RightMouseDown { get; set; }
        public bool DrawMouse { get; set; } = true;

        public bool CanPress { get; set; }
        public bool CanType { get; set; }

        public Texture2D Image { get; set; }
        public Texture2D ImageNormal { get; set; }
        public Texture2D ImageClick { get; set; }

        public Vector2 Position { get; set; }

        public Point Size { get; set; }

        public Rectangle Hitbox { get; set; }
    }
}
