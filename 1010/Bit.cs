using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace _1010 {
    public class Bit : GameObject {
        public Bit(Color color, Point coords, PlayBlock block) {
            Color = color;
            Block = block;

            LocalCoordinates = coords;

            Position = Mouse.Position + (LocalCoordinates.ToVector2() * (Block.BlockSize.ToVector2() + new Vector2(Block.Spacing)));
            DrawPosition = Position;
        }

        public void Update(GameTime gt) {
            if (!FullyPlaced) {
                if (!Placed)
                    Position = Mouse.Position + (LocalCoordinates.ToVector2() * (Block.BlockSize.ToVector2() + new Vector2(Block.Spacing)));
                else if (DrawPosition - Position == Vector2.Zero)
                    FullyPlaced = true;
                
                DrawPosition -= (DrawPosition - Position) * 25 * (float) gt.ElapsedGameTime.TotalSeconds;
            }
        }

        public void DrawHover(SpriteBatch sb) {
            if (!Placed) {
                sb.Draw(BlankPixel, DrawPosition - new Vector2(Block.Spacing), new Rectangle(Point.Zero, Block.BlockSize + new Point(Block.Spacing * 2)), Color.White);

                if (Block.CanPlaceBits())
                    sb.Draw(BlankPixel, PlacedPosition, new Rectangle(Point.Zero, Block.BlockSize), new Color(Color, .7f));
            }
        }
        public void Draw(SpriteBatch sb) {
            sb.Draw(BlankPixel, DrawPosition, new Rectangle(Point.Zero, Block.BlockSize), Color);
        }

        public bool FullyPlaced { get; set; } = false;
        public bool Placed { get; set; } = false;

        public Point LocalCoordinates { get; set; }
        public Point Coordinates {
            get {
                return new Vector2(((Position.X - Block.Position.X) - (Position.X - Block.Position.X) % (Block.BlockSize.X + Block.Spacing)) / (Block.BlockSize.X + Block.Spacing), ((Position.Y - Block.Position.Y) - (Position.Y - Block.Position.Y) % (Block.BlockSize.Y + Block.Spacing)) / (Block.BlockSize.Y + Block.Spacing)).ToPoint();
            }
        }

        public Vector2 Position { get; set; }
        public Vector2 DrawPosition { get; set; }
        public Vector2 PlacedPosition {
            get {
                //return Position - new Vector2((Position.X - Block.Position.X) % (Block.BlockSize.X + Block.Spacing), (Position.Y - Block.Position.Y) % (Block.BlockSize.Y + Block.Spacing));
                return Block.Position + new Vector2(Block.Spacing + (Block.BlockSize.X + Block.Spacing) * Coordinates.X, Block.Spacing + (Block.BlockSize.Y + Block.Spacing) * Coordinates.Y);
            }
        }

        public PlayBlock Block { get; set; }

        public Color Color { get; set; }
    }
}
