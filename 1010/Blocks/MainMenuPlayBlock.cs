using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010 {
    public class MainMenuPlayBlock : PlacementBlock {
        public MainMenuPlayBlock(List<int[]> spots, Vector2 position, List<int[]> obstacles = null) : base(spots, position, obstacles) {
            
        }

        public override void Update(GameTime gt) {
            //if (SafeHitbox.Contains(Mouse.Position))
                base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            DrawPlacingBits = SafeHitbox.Contains(Mouse.Position);

            base.Draw(sb);
        }
    }
}
