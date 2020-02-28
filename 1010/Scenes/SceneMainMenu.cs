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
            BlockCreationHitbox = new Rectangle(new Point(140, 20), new Point(100));

            Blocks = new List<MainMenuPlayBlock>() {
                new MainMenuPlayBlock(new List<int[]> {
					new int[] { 1, 0 },
				new int[] { 1, 1 },
				new int[] { 1, 2 },
				new int[] { 2, 1 },
				new int[] { 2, 2 },
				new int[] { 2, 0 },
				new int[] { 0, 1 },
				new int[] { 0, 2 },
				new int[] { 1, 3 },
				new int[] { 2, 3 },
				new int[] { 3, 2 },
				new int[] { 3, 1 },
				new int[] { 2, 5 },
				new int[] { 1, 5 },
				new int[] { 1, 6 },
				new int[] { 2, 6 },
				new int[] { 0, 6 },
				new int[] { 0, 7 },
				new int[] { 1, 8 },
				new int[] { 1, 7 },
				new int[] { 2, 7 },
				new int[] { 2, 8 },
				new int[] { 3, 7 },
				new int[] { 3, 6 },
				new int[] { 5, 6 },
				new int[] { 5, 7 },
				new int[] { 6, 6 },
				new int[] { 6, 7 },
				new int[] { 6, 5 },
				new int[] { 7, 5 },
				new int[] { 8, 6 },
				new int[] { 7, 6 },
				new int[] { 7, 7 },
				new int[] { 8, 7 },
				new int[] { 7, 8 },
				new int[] { 6, 3 },
				new int[] { 5, 2 },
				new int[] { 6, 2 },
				new int[] { 5, 1 },
				new int[] { 6, 1 },
				new int[] { 6, 0 },
				new int[] { 7, 1 },
				new int[] { 7, 2 },
				new int[] { 7, 3 },
				new int[] { 8, 2 },
				new int[] { 8, 1 },
				new int[] { 7, 0 },
				new int[] { 3, 5 },
				new int[] { 3, 3 },
				new int[] { 3, 4 },
				new int[] { 4, 3 },
				new int[] { 5, 3 },
				new int[] { 5, 4 },
				new int[] { 5, 5 },
				new int[] { 4, 5 },
				new int[] { 4, 4 },
				new int[] { 6, 8 },
				}, new Vector2(500, 200), new List<int[]> {
                    new int[] { 4, 4 },
                })
            };
        }
        public void ChangeState(Action action, State state) {
            Action = action;
            State = state;            
        }

        public override void Update(GameTime gt) {
            if (Mouse.LeftMouseDown && Mouse.CanPress) {
                if (PlayHitbox.Contains(Mouse.Position))
                    Manager.SwitchScene(new SceneGame());
                else if (BlockCreationHitbox.Contains(Mouse.Position))
                    Manager.SwitchScene(new SceneBlockCreation());
            }

            Blocks.ForEach(x => x.Update(gt));

			Mouse.DrawMouse = !Blocks.Any(x => x.SafeHitbox.Contains(Mouse.Position));

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            sb.Draw(BlankPixel, PlayHitbox.Location.ToVector2(), new Rectangle(Point.Zero, PlayHitbox.Size), Color.Gray);
            sb.DrawString(FontMedium, "Play", new Vector2(30), Color.White);

            sb.Draw(BlankPixel, BlockCreationHitbox.Location.ToVector2(), new Rectangle(Point.Zero, BlockCreationHitbox.Size), Color.Gray);
            sb.DrawString(FontMedium, "Block\nCreation", new Vector2(150, 20), Color.White);

            Blocks.ForEach(x => x.Draw(sb));
        }

        public List<MainMenuPlayBlock> Blocks { get; set; }

        public Rectangle PlayHitbox { get; set; }
        public Rectangle BlockCreationHitbox { get; set; }
    }
}