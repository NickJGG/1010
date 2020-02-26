using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1010 {
    public class Block : GameObject {
        public Block(Point gridSize) {
            GridSize = gridSize;
            //Position = new Vector2(Graphics.PreferredBackBufferWidth / 2 - Size.X / 2, Graphics.PreferredBackBufferHeight / 2 - Size.Y / 2);

            Bits = new Bit[GridSize.X, GridSize.Y];
            PlacingBits = new List<Bit>();
        }
        public void AddBits(BlockType data) {
            PlacingBits = new List<Bit>();

            for (int i = 0; i < data.Values.GetLength(0); i++) {
                for (int j = 0; j < data.Values.GetLength(1); j++) {
                    if (data.Values[i, j] == 1)
                        PlacingBits.Add(new Bit(data.Color, new Point(i, j), this));
                }
            }
        }

        public void Update(GameTime gt) {
            bool gen = false;

            List<int> rows = new List<int>(), columns = new List<int>();

            for (int i = 0; i < Bits.GetLength(0); i++) {
                int amount = 0;

                for (int j = 0; j < Bits.GetLength(1); j++) {
                    if (Bits[i, j] != null) {
                        Bits[i, j].Update(gt);
                        amount++;
                    }
                }

                if (amount == GridSize.Y) {
                    columns.Add(i);
                }
            }
            for (int i = 0; i < Bits.GetLength(1); i++) {
                int amount = 0;

                for (int j = 0; j < Bits.GetLength(0); j++) {
                    if (Bits[j, i] != null) {
                        amount++;
                    }
                }

                if (amount == GridSize.X) {
                    rows.Add(i);
                }
            }

            for (int i = 0; i < columns.Count; i++) {
                for (int j = 0; j < GridSize.Y; j++) {
                    Bits[columns[i], j] = null;
                }
            }
            for (int i = 0; i < GridSize.X; i++) {
                for (int j = 0; j < rows.Count; j++) {
                    Bits[i, rows[j]] = null;
                }
            }

            PlacingBits.ForEach(x => x.Update(gt));

            if (Mouse.LeftMouseDown && Mouse.CanPress) {
                bool place = true;

                foreach (Bit b in PlacingBits) {
                    if (InGrid(b)) {
                        if (Bits[b.RealCoordinates.X, b.RealCoordinates.Y] != null) {
                            place = false;

                            break;
                        }
                    } else
                        place = false;
                }

                if (place) {
                    Place();

                    AddBits(Combos[Utilities.Next(0, Combos.Count)]);
                }
            }
        }
        public bool InGrid(Bit b) {
            return b.RealCoordinates.X >= 0 && b.RealCoordinates.X < GridSize.X && b.RealCoordinates.Y >= 0 && b.RealCoordinates.Y < GridSize.Y;
        }
        public void Place() {
            int x = (int) (Mouse.Position.X - Mouse.Position.X % BlockSize.X) / BlockSize.X, y = (int) (Mouse.Position.Y - Mouse.Position.Y % BlockSize.Y) / BlockSize.Y;

            foreach (Bit b in PlacingBits) {
                //b.Position = new Vector2(Mouse.Position.X - Mouse.Position.X % BlockSize.X, Mouse.Position.Y - Mouse.Position.Y % BlockSize.Y);
                b.Position = Position + new Vector2(Spacing + (BlockSize.X + Spacing) * b.RealCoordinates.X, Spacing + (BlockSize.Y + Spacing) * b.RealCoordinates.Y);

                Bits[b.RealCoordinates.X, b.RealCoordinates.Y] = new Bit(b.Color, b.RealCoordinates, this) { Position = b.Position, Placed = true };
            }
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(BlankPixel, Position, new Rectangle(Point.Zero, Size), Color.FromNonPremultiplied(235, 235, 235, 255));

            for (int i = 0; i <= GridSize.X; i++) {
                sb.Draw(BlankPixel, Position + new Vector2((BlockSize.X + Spacing) * i, 0), new Rectangle(Point.Zero, new Point(Spacing, Size.Y)), Color.White);
                sb.Draw(BlankPixel, Position + new Vector2(0, (BlockSize.Y + Spacing) * i), new Rectangle(Point.Zero, new Point(Size.X, Spacing)), Color.White);
            }

            for (int i = 0; i < Bits.GetLength(0); i++) {
                for (int j = 0; j < Bits.GetLength(1); j++) {
                    Bits[i, j]?.Draw(sb);
                }
            }

            PlacingBits.ForEach(x => x.Draw(sb));
        }

        public int Spacing { get; set; } = 2;

        public Point BlockSize { get; set; } = new Point(25);
        public Point GridSize { get; set; } = new Point(10);
        public Point Size {
            get { return new Point(GridSize.X * BlockSize.X + (GridSize.X + 1) * Spacing, GridSize.Y * BlockSize.Y + (GridSize.Y + 1) * Spacing); }
        }

        public Vector2 Position { get; set; }

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
        public List<Bit> PlacingBits { get; set; }
    }
}
