using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010 {
    public class ObstacleCreationBlock : Block {
        public ObstacleCreationBlock(Point gridSize, List<int[]> obstacles = null) : base(gridSize, obstacles) {

        }

        public override void Update(GameTime gt) {
            if (Mouse.LeftMouseDown && Mouse.CanPress) {
                Point coords = GetCoordinates(Mouse.Position);

                if (InGrid(coords)) {
                    bool place = true;

                    foreach (int[] pair in Obstacles) {
                        if (pair[0] == coords.X && pair[1] == coords.Y) {
                            Obstacles.Remove(pair);

                            place = false;

                            break;
                        }
                    }

                    if (place)
                        Obstacles.Add(new int[2] { coords.X, coords.Y });

                    Console.WriteLine("\nNew Obstacles:");
                    Obstacles.ForEach(x => Console.WriteLine("\t\t\t\tnew int[] { " + x[0] + ", " + x[1] + " },"));
                }
            }
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);
        }
    }
}
