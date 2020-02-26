using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1010 {
    public class Save : GameObject {
        public Save() {
            Block = new Block(new Point(17));
            //Game1.SetSize(Block.Size);
            Block.Position = new Vector2(Graphics.PreferredBackBufferWidth / 2 - Block.Size.X / 2, Graphics.PreferredBackBufferHeight / 2 - Block.Size.Y / 2);
        }

        public void Update(GameTime gt) {
            Block.Update(gt);
        }

        public void Draw(SpriteBatch sb) {
            Block.Draw(sb);
        }

        public Block Block { get; set; }
    }
}
