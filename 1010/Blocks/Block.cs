using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _1010 {
    public class Block : GameObject {
        public Block(List<int[]> spots, Vector2? position = null, List<int[]> obstacles = null) {
            Spots = spots;

            Obstacles = obstacles ?? new List<int[]>();

            //Position = new Vector2(Graphics.PreferredBackBufferWidth / 2 - RealSize.X / 2, Graphics.PreferredBackBufferHeight / 2 - RealSize.Y / 2);
            Position = position ?? new Vector2(500, 200);

            int largeX = 0, largeY = 0;

            foreach (int[] spot in spots) {
                if (spot[0] > largeX)
                    largeX = spot[0];

                if (spot[1] > largeY)
                    largeY = spot[1];
            }

            RealSize = new Point((BlockSize.X + Spacing) * ++largeX, (BlockSize.Y + Spacing) * ++largeY);
            Hitbox = new Rectangle(Position.ToPoint(), RealSize);
            SafeHitbox = new Rectangle(Position.ToPoint() - BlockSize, RealSize + BlockSize * new Point(2));
        }

        public virtual void Update(GameTime gt) {
            Mouse.DrawMouse = false;
        }

        public bool InList(List<int[]> list, int x, int y) {
            return list.Any(o => o.SequenceEqual(new int[2] { x, y }));
        }
        public bool SpotExists(Point coords) {
            return InList(Spots, coords.X, coords.Y);
        }
        public bool ObstacleExists(int x, int y) {
            return InList(Obstacles, x, y);
        }

        public Point GetCoordinates(Vector2 position) {
            return new Vector2(((Mouse.Position.X - Position.X) - (Mouse.Position.X - Position.X) % (BlockSize.X + Spacing)) / (BlockSize.X + Spacing), ((Mouse.Position.Y - Position.Y) - (Mouse.Position.Y - Position.Y) % (BlockSize.Y + Spacing)) / (BlockSize.Y + Spacing)).ToPoint();
        }

        public virtual void Draw(SpriteBatch sb) {
            foreach (int[] coords in Spots) {
                sb.Draw(BlankPixel, Position + new Vector2((BlockSize.X + Spacing) * coords[0], (BlockSize.Y + Spacing) * coords[1]), new Rectangle(Point.Zero, BlockSize + new Point(Spacing * 2)), Color.FromNonPremultiplied(235, 235, 235, 255));
                sb.Draw(BlankPixel, Position + new Vector2(Spacing + (BlockSize.X + Spacing) * coords[0], Spacing + (BlockSize.Y + Spacing) * coords[1]), new Rectangle(Point.Zero, BlockSize), Color.White);
            }

            foreach (int[] coords in Obstacles) {
                sb.Draw(BlankPixel, Position + new Vector2(Spacing + (BlockSize.X + Spacing) * coords[0], Spacing + (BlockSize.Y + Spacing) * coords[1]), new Rectangle(Point.Zero, BlockSize), new Color(Color.Black, .8f));
            }
        }

        public int Spacing { get; set; } = 2;

        public Point BlockSize { get; set; } = new Point(25);
        public Point RealSize { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Hitbox { get; set; }
        public Rectangle SafeHitbox { get; set; }

        public List<int[]> Spots { get; set; }
        public List<int[]> Obstacles { get; set; }
    }
}
