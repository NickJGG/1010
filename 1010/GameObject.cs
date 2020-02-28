using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;

namespace _1010 {
    public abstract class GameObject {
        public GameObject() {

        }

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics, Camera2D camera, GameMouse mouse, Game1 game) {
            Content = content;
            Graphics = graphics;
            Camera = camera;
            Mouse = mouse;
            Game1 = game;

            BlankPixel = Content.Load<Texture2D>("blankPixel");
            Spritesheet = Content.Load<Texture2D>("spritesheet");

            FontSmall = Content.Load<SpriteFont>("fonts/roboto/RegularSmall");
            FontMedium = Content.Load<SpriteFont>("fonts/roboto/RegularMedium");
            FontLarge = Content.Load<SpriteFont>("fonts/silkscreen/large");
            FontTiny = Content.Load<SpriteFont>("fonts/silkscreen/tiny");
            Font15 = Content.Load<SpriteFont>("fonts/silkscreen/font15");
        }
        
        public static Texture2D Crop(Texture2D image, Rectangle source) {
            var graphics = image.GraphicsDevice;
            var ret = new RenderTarget2D(graphics, source.Width, source.Height);
            var sb = new SpriteBatch(graphics);

            graphics.SetRenderTarget(ret);
            graphics.Clear(new Color(0, 0, 0, 0));

            sb.Begin();
            sb.Draw(image, Vector2.Zero, source, Color.White);
            sb.End();

            graphics.SetRenderTarget(null);

            return (Texture2D) ret;
        }
        public static Texture2D GenerateTexture(int width, int height) {
            Color[] data = new Color[width * height];
            Texture2D texture = new Texture2D(Graphics.GraphicsDevice, width, height);

            //Color color = Color.FromNonPremultiplied(Utilities.Next(0, 255), Utilities.Next(0, 255), Utilities.Next(0, 255), 255);
            Color color = Color.FromNonPremultiplied(50, 50, 50, 255);

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    data[i * width + j] = color;
                }
            }

            texture.SetData(data);

            return texture;
        }

        public static int Cell {
            get { return 30; }
        }

        public static Point SpritesheetSize {
            get { return new Point(Spritesheet.Width / Cell, Spritesheet.Height / Cell); }
        }

        public static ContentManager Content { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static Camera2D Camera { get; set; }
        public static GameMouse Mouse { get; set; }
        public static Game1 Game1 { get; set; }

        public static Texture2D BlankPixel { get; set; }
        public static Texture2D Spritesheet { get; set; }

        public static SpriteFont FontLarge { get; set; }
        public static SpriteFont FontMedium { get; set; }
        public static SpriteFont FontSmall { get; set; }
        public static SpriteFont FontTiny { get; set; }
        public static SpriteFont Font15 { get; set; }
    }
}
