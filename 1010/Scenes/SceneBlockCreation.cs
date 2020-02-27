using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace _1010 {
    public class SceneBlockCreation : Scene {
        public SceneBlockCreation() : base("Block Creation") {
            Block = new ObstacleCreationBlock(new List<int[]>() {
                new int[] { 0, 0 },
                new int[] { 0, 1 },
                new int[] { 1, 0 },
                new int[] { 1, 1 },
            });

            Camera.Zoom = 1f;
        }

        public override void Update(GameTime gt) {
            Block.Update(gt);

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            Block.Draw(sb);

            base.Draw(sb);
        }

        public ObstacleCreationBlock Block { get; set; }
    }
}