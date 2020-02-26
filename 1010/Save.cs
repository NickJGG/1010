using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1010 {
    public class Save : GameObject {
        public Save() {
            Block = new PlayBlock(new Point(12), new List<int[]>() {
				new int[] { 4, 4 },
				new int[] { 5, 4 },
				new int[] { 5, 5 },
				new int[] { 4, 5 },
				new int[] { 6, 5 },
				new int[] { 6, 4 },
				new int[] { 6, 6 },
				new int[] { 5, 6 },
				new int[] { 5, 7 },
				new int[] { 6, 7 },
				new int[] { 6, 8 },
				new int[] { 5, 3 },
				new int[] { 4, 2 },
				new int[] { 4, 3 },
				new int[] { 5, 2 },
				new int[] { 4, 1 },
				new int[] { 3, 11 },
				new int[] { 3, 10 },
				new int[] { 4, 11 },
				new int[] { 8, 0 },
				new int[] { 8, 1 },
				new int[] { 9, 0 },
				new int[] { 11, 8 },
				new int[] { 10, 8 },
				new int[] { 10, 7 },
				new int[] { 10, 6 },
				new int[] { 9, 6 },
				new int[] { 11, 7 },
				new int[] { 0, 6 },
				new int[] { 0, 5 },
				new int[] { 1, 5 },
				new int[] { 1, 6 },
				new int[] { 2, 5 },
				new int[] { 2, 4 },
			});
            //Game1.SetSize(Block.Size);
        }
        public Save(PlayBlock block) {
            Block = block;

            Block.Position = new Vector2(Graphics.PreferredBackBufferWidth / 2 - Block.RealSize.X / 2, Graphics.PreferredBackBufferHeight / 2 - Block.RealSize.Y / 2);
        }

        public void Update(GameTime gt) {
            Block.Update(gt);
        }

        public void Draw(SpriteBatch sb) {
            Block.Draw(sb);
        }

        public PlayBlock Block { get; set; }
    }
}
