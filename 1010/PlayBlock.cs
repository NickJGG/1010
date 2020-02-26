using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1010 {
    public class PlayBlock : Block {
        public PlayBlock(Point gridSize, List<int[]> obstacles = null) : base(gridSize, obstacles) {
            Bits = new Bit[gridSize.X, gridSize.Y];
        }

        public override void Update(GameTime gt) {
            base.Update(gt);

            if (Placing) {
                PlacingBits.ForEach(x => x.Update(gt));

                if (Mouse.LeftMouseDown && Mouse.CanPress) {
                    if (CanPlaceBits()) {
                        PlaceBits();
                    }
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

        public bool InGrid(Bit b) {
            return InGrid(b.Coordinates);
        }
        public bool HitsOtherBits(Bit b) {
            for (int i = 0; i < GridSize.X; i++) {
                for (int j = 0; j < GridSize.Y; j++) {
                    if (Bits[i, j] != null && i == b.Coordinates.X && j == b.Coordinates.Y)
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
            return InGrid(b) && !HitsOtherBits(b) && !HitsObstacles(b);
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
            foreach (Bit b in PlacingBits) {
                b.Position = b.PlacedPosition;

                Bit newBit = new Bit(b.Color, b.LocalCoordinates, this) { Placed = true, Position = b.Position };

                Bits[b.Coordinates.X, b.Coordinates.Y] = newBit;

                FinalizingBits.Add(newBit);
            }

            Placing = false;

            CheckForClears();
        }
        public void CheckForClears() {
            List<int> rows = new List<int>(),
                        cols = new List<int>();

            for (int i = 0; i < GridSize.X; i++) { // Check if there is a clearance in the X axis
                bool clear = true;

                for (int j = 0; j < GridSize.Y && clear; j++) {
                    if (Bits[i, j] == null && !ObstacleExists(i, j)) {
                        clear = false;
                    }
                }

                if (clear)
                    rows.Add(i);
            }

            for (int i = 0; i < GridSize.Y; i++) {// Check if there is a clearance in the Y axis
                bool clear = true;

                for (int j = 0; j < GridSize.X && clear; j++) {
                    if (Bits[j, i] == null && !ObstacleExists(j, i)) {
                        clear = false;
                    }
                }

                if (clear)
                    cols.Add(i);
            }

            foreach (int row in rows) { // Clear all completions
                for (int i = 0; i < GridSize.Y; i++) {
                    Bits[row, i] = null;
                }
            }

            foreach (int col in cols) { // Clear all completions
                for (int i = 0; i < GridSize.X; i++) {
                    Bits[i, col] = null;
                }
            }
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            for (int i = 0; i < Bits.GetLength(0); i++) {
                for (int j = 0; j < Bits.GetLength(1); j++) {
                    Bits[i, j]?.Draw(sb);
                }
            }

            if (Placing) {
                PlacingBits.ForEach(x => x.DrawHover(sb));
                PlacingBits.ForEach(x => x.Draw(sb));
            }
        }

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

        public Bit[,] Bits { get; set; }

        public List<Bit> PlacingBits { get; set; } = new List<Bit>();
        public List<Bit> FinalizingBits { get; set; } = new List<Bit>();
    }
}
