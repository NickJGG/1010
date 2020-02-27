using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010 {
    public class PlayBlock : Block {
        public PlayBlock(List<int[]> spots, List<int[]> obstacles = null) : base(spots, obstacles) {
            Bits = new List<Bit>();
        }

        public override void Update(GameTime gt) {
            base.Update(gt);

            if (Placing) {
                PlacingBits.ForEach(x => x.Update(gt));

                if (Mouse.LeftMouseDown && Mouse.CanPress) {
                    Console.WriteLine(CanPlaceBits());

                    foreach (Bit b in PlacingBits) {
                        if (HitsOtherBits(b)) {
                            //Console.WriteLine(b.LocalCoordinates.ToString());
                            //Console.WriteLine(b.Coordinates.ToString());
                        }
                    }

                    if (CanPlaceBits()) {
                        PlaceBits();
                    }

                    foreach (Bit bit in Bits)
                        Console.WriteLine("PLACED BIT: " + bit.Coordinates);
                }
            } else {
                StartPlacing();
            }

            if (FinalizingBits.Count > 0) {
                List<Bit> bitsToRemove = new List<Bit>();

                foreach (Bit b in FinalizingBits) {
                    if (b.FullyPlaced)
                        bitsToRemove.Add(b);
                    else
                        b.Update(gt);
                }

                bitsToRemove.ForEach(x => FinalizingBits.Remove(x));
            }
        }
        
        public bool HitsOtherBits(Bit b) {
            /*for (int i = 0; i < GridSize.X; i++) {
                for (int j = 0; j < GridSize.Y; j++) {
                    if (Bits[i, j] != null && i == b.Coordinates.X && j == b.Coordinates.Y)
                        return true;
                }
            }*/

            foreach (Bit bit in Bits) {
                if (bit.Coordinates.X == b.Coordinates.X && bit.Coordinates.Y == b.Coordinates.Y) {
                    //Console.WriteLine(bit.Coordinates.ToString() + " // " + b.Coordinates.ToString()    );

                    return true;
                }
            }

            return false;
        }
        public bool HitsObstacles(Bit b) {
            foreach (int[] coords in Obstacles) {
                if (b.Coordinates == new Point(coords[0], coords[1])) {
                    return true;
                }
            }

            return false;
        }
        public bool CanPlaceBit(Bit b) {
            return SpotExists(b.Coordinates) && !HitsOtherBits(b) && !HitsObstacles(b);
        }
        public bool CanPlaceBits() {
            foreach (Bit b in PlacingBits) {
                if (!CanPlaceBit(b))
                    return false;
            }

            return true;
        }

        public void StartPlacing() {
            PlacingBits = new List<Bit>();

            BlockType type = Combos[Utilities.Next(0, Combos.Count - 1)];

            for (int i = 0; i < type.Values.GetLength(0); i++) {
                for (int j = 0; j < type.Values.GetLength(1); j++) {
                    if (type.Values[i, j] == 1)
                        PlacingBits.Add(new Bit(type.Color, new Point(i, j), this));
                }
            }

            Placing = true;
        }
        public void PlaceBits() {
            List<int> rowsToCheck = new List<int>(), colsToCheck = new List<int>();

            foreach (Bit b in PlacingBits) {
                b.Position = b.PlacedPosition;

                Bit newBit = new Bit(b.Color, b.LocalCoordinates, this) { Placed = true, Position = b.Position };

                Bits.Add(newBit);

                FinalizingBits.Add(newBit);

                if (!rowsToCheck.Contains(b.Coordinates.X))
                    rowsToCheck.Add(b.Coordinates.X);
                
                if (!colsToCheck.Contains(b.Coordinates.Y))
                    colsToCheck.Add(b.Coordinates.Y);
            }

            Placing = false;

            CheckForClears(rowsToCheck, colsToCheck);
        }
        public void CheckForClears(List<int> rowsToCheck, List<int> colsToCheck) {
            int score = 0, count = 0;
            
            List<int> rowsToRemove = new List<int>(), colsToRemove = new List<int>();

            foreach (int i in rowsToCheck) {
                int numSpots = Spots.Where(x => x[0] == i).Count() - Obstacles.Where(x => x[0] == i).Count();

                IEnumerable<Bit> matches = Bits.Where(x => x.Coordinates.X == i);

                if (numSpots == matches.Count()) {
                    rowsToRemove.Add(i);

                    score += matches.Count();
                    count++;
                }
            }

            foreach (int i in colsToCheck) {
                int numSpots = Spots.Where(x => x[1] == i).Count() - Obstacles.Where(x => x[1] == i).Count();

                IEnumerable<Bit> matches = Bits.Where(x => x.Coordinates.Y == i);

                if (numSpots == matches.Count()) {
                    colsToRemove.Add(i);

                    score += matches.Count();
                    count++;
                }
            }

            foreach (int i in rowsToRemove)
                Bits.RemoveAll(x => x.Coordinates.X == i);

            foreach (int i in colsToRemove)
                Bits.RemoveAll(x => x.Coordinates.Y == i);

            Score += score * count;
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            Bits.ForEach(x => x.Draw(sb));

            if (Placing) {
                PlacingBits.ForEach(x => x.DrawHover(sb));
                PlacingBits.ForEach(x => x.Draw(sb));
            }

            sb.DrawString(FontMedium, "Score: " + Score, new Vector2(30), Color.Black);
        }

        public int Score { get; set; }

        public bool Placing { get; set; }

        public static List<BlockType> Combos { get; set; } = new List<BlockType>() {
                new BlockType(new int[,] { {1, 1}, {1, 1} }, Color.FromNonPremultiplied(153, 219, 83, 255)),
                new BlockType(new int[,] { {1}, {1}, {1}, {1}, {1} }, Color.FromNonPremultiplied(220, 101, 85, 255)),
                new BlockType(new int[,] { {1, 1, 1, 1, 1} }, Color.FromNonPremultiplied(220, 101, 85, 255)),
                new BlockType(new int[,] { {1, 1, 1, 1} }, Color.FromNonPremultiplied(230, 106, 130, 255)),
                new BlockType(new int[,] { {1}, {1}, {1}, {1} }, Color.FromNonPremultiplied(230, 106, 130, 255)),
                new BlockType(new int[,] { {1, 1, 1} }, Color.FromNonPremultiplied(235, 149, 72, 255)),
                new BlockType(new int[,] { {1}, {1}, {1} }, Color.FromNonPremultiplied(235, 149, 72, 255)),
                new BlockType(new int[,] { {1, 1, 1}, {1, 0, 0}, {1, 0, 0} }, Color.FromNonPremultiplied(92, 190, 228, 255)),
                new BlockType(new int[,] { {1, 0, 0}, {1, 0, 0}, {1, 1, 1} }, Color.FromNonPremultiplied(92, 190, 228, 255)),
                new BlockType(new int[,] { {0, 0, 1}, {0, 0, 1}, {1, 1, 1} }, Color.FromNonPremultiplied(92, 190, 228, 255)),
                new BlockType(new int[,] { {1, 1, 1}, {0, 0, 1}, {0, 0, 1} }, Color.FromNonPremultiplied(92, 190, 228, 255)),
                new BlockType(new int[,] { {1, 1} }, Color.FromNonPremultiplied(135, 149, 216, 255)),
                new BlockType(new int[,] { {1}, {1} }, Color.FromNonPremultiplied(135, 149, 216, 255)),
                new BlockType(new int[,] { { 1 } }, Color.FromNonPremultiplied(135, 149, 216, 255)),
                new BlockType(new int[,] { {1, 1, 1}, {1, 1, 1}, {1, 1, 1} }, Color.FromNonPremultiplied(109, 221, 190, 255)),
                new BlockType(new int[,] { {1, 1}, {1, 0} }, Color.FromNonPremultiplied(87, 202, 132, 255)),
                new BlockType(new int[,] { {0, 1}, {1, 1} }, Color.FromNonPremultiplied(87, 202, 132, 255)),
                new BlockType(new int[,] { {1, 0}, {1, 1} }, Color.FromNonPremultiplied(87, 202, 132, 255)),
                new BlockType(new int[,] { {1, 1}, {0, 1} }, Color.FromNonPremultiplied(87, 202, 132, 255)),
            };

        public List<Bit> PlacingBits { get; set; } = new List<Bit>();
        public List<Bit> FinalizingBits { get; set; } = new List<Bit>();
        public List<Bit> Bits { get; set; } = new List<Bit>();
    }
}
