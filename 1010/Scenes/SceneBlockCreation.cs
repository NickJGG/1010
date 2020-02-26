using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace _1010 {
    public class SceneBlockCreation : Scene {
        public SceneBlockCreation(SceneManager manager) : base("Block Creation") {
            Camera.Zoom = 1f;
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);
        }
    }
}