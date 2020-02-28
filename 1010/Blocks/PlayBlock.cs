using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010 {
    public class PlayBlock : PlacementBlock {
        public PlayBlock(List<int[]> spots, Vector2? position = null, List<int[]> obstacles = null) : base(spots, position, obstacles) {
            
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            sb.DrawString(FontMedium, "Score: " + Score, new Vector2(30), Color.Black);
        }
    }
}
