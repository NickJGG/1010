using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1010 {
    public class SceneMainMenu : Scene {
        public SceneMainMenu() : base("Main Menu") {
            PlayHitbox = new Rectangle(new Point(20), new Point(100));
        }
        public void ChangeState(Action action, State state) {
            Action = action;
            State = state;            
        }

        public override void Update(GameTime gt) {
            if (Mouse.LeftMouseDown && Mouse.CanPress) {
                if (PlayHitbox.Contains(Mouse.Position))
                    Manager.SwitchScene(new SceneGame());
            }

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            sb.Draw(BlankPixel, PlayHitbox.Location.ToVector2(), new Rectangle(Point.Zero, PlayHitbox.Size), Color.Gray);
            sb.DrawString(FontMedium, "Play", new Vector2(30), Color.White);
        }

        public Rectangle PlayHitbox { get; set; }
    }
}