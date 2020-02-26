using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1010 {
    public class Block : GameObject {
        public Block(Point gridSize, List<int[]> obstacles = null) {
            GridSize = gridSize;

            Obstacles = obstacles ?? new List<int[]>();

            Position = new Vector2(Graphics.PreferredBackBufferWidth / 2 - RealSize.X / 2, Graphics.PreferredBackBufferHeight / 2 - RealSize.Y / 2);
        }

        public virtual void Update(GameTime gt) {
            
        }

        public bool InGrid(Point coords) {
            return coords.X >= 0 && coords.X < GridSize.X && coords.Y >= 0 && coords.Y < GridSize.Y;
        }
        public bool ObstacleExists(int i, int j) {
            return Obstacles.Any(x => x.SequenceEqual(new int[2] { i, j }));
        }

        public Point GetCoordinates(Vector2 position) {
            return new Vector2(((Mouse.Position.X - Position.X) - (Mouse.Position.X - Position.X) % (BlockSize.X + Spacing)) / (BlockSize.X + Spacing), ((Mouse.Position.Y - Position.Y) - (Mouse.Position.Y - Position.Y) % (BlockSize.Y + Spacing)) / (BlockSize.Y + Spacing)).ToPoint();
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(BlankPixel, Position, new Rectangle(Point.Zero, RealSize), Color.FromNonPremultiplied(235, 235, 235, 255)); // Draw background

            for (int i = 0; i <= GridSize.X; i++) { // Draw grid lines
                sb.Draw(BlankPixel, Position + new Vector2((BlockSize.X + Spacing) * i, 0), new Rectangle(Point.Zero, new Point(Spacing, RealSize.Y)), Color.White);
                sb.Draw(BlankPixel, Position + new Vector2(0, (BlockSize.Y + Spacing) * i), new Rectangle(Point.Zero, new Point(RealSize.X, Spacing)), Color.White);
            }            

            foreach (int[] coords in Obstacles) {
                sb.Draw(BlankPixel, Position + new Vector2(Spacing + (BlockSize.X + Spacing) * coords[0], Spacing + (BlockSize.Y + Spacing) * coords[1]), new Rectangle(Point.Zero, BlockSize), new Color(Color.Black, .8f));
            }
        }

        public int Spacing { get; set; } = 2;

        public Point BlockSize { get; set; } = new Point(25);
        public Point GridSize { get; set; } = new Point(10);
        public Point RealSize {
            get { return new Point(GridSize.X * BlockSize.X + (GridSize.X + 1) * Spacing, GridSize.Y * BlockSize.Y + (GridSize.Y + 1) * Spacing); }
        }

        public Vector2 Position { get; set; }

        public List<int[]> Obstacles { get; set; }
    }
}
