using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace _1010 {
    public class SceneGame : Scene { 
        public SceneGame() : base("Game") {
            Save = new Save();

            Camera.Zoom = 1f;
            Mouse.DrawMouse = false;
        }

        public override void Update(GameTime gt) {
            Save.Update(gt);

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            Save.Draw(sb);

            base.Draw(sb);
        }

        public Save Save { get; set; }
    }
}