using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010 {
    public class ObstacleCreationBlock : Block {
        public ObstacleCreationBlock(List<int[]> spots, List<int[]> obstacles = null) : base(spots, null, obstacles) {

        }

        public override void Update(GameTime gt) {
            if ((Mouse.LeftMouseDown || Mouse.RightMouseDown) && Mouse.CanPress) {
                Point coords = GetCoordinates(Mouse.Position);

                if (Mouse.LeftMouseDown) {
                    if (SpotExists(coords)) {
                        foreach (int[] pair in Spots) {
                            if (pair[0] == coords.X && pair[1] == coords.Y) {
                                Spots.Remove(pair);

                                break;
                            }
                        }
                    } else
                        Spots.Add(new int[2] { coords.X, coords.Y });

                    Console.WriteLine("\n=========================================\nNew Spots:");
                    Spots.ForEach(x => Console.WriteLine("\t\t\t\tnew int[] { " + x[0] + ", " + x[1] + " },"));
                } else {
                    if (SpotExists(coords)) {
                        if (ObstacleExists(coords.X, coords.Y)) {
                            foreach (int[] pair in Obstacles) {
                                if (pair[0] == coords.X && pair[1] == coords.Y) {
                                    Obstacles.Remove(pair);

                                    break;
                                }
                            }
                        } else
                            Obstacles.Add(new int[2] { coords.X, coords.Y });

                        Console.WriteLine("\n=========================================\nNew Obstacles:");
                        Obstacles.ForEach(x => Console.WriteLine("\t\t\t\tnew int[] { " + x[0] + ", " + x[1] + " },"));
                    }
                }
            }
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);
        }
    }
}
