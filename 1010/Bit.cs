using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _1010 {
    public class Bit : GameObject {
        public Bit(Color color, Point coords, Block block) {
            Coordinates = coords;
            RealCoordinates = coords;

            Position = Mouse.Position;
            DrawPosition = Position;

            Color = color;

            Block = block;
        }

        public void Update(GameTime gt) {
            if (!Placed)
                Position = Mouse.Position + (Coordinates.ToVector2() * new Vector2(Block.BlockSize.X + Block.Spacing, Block.BlockSize.Y + Block.Spacing));
               
            DrawPosition -= (DrawPosition - Position) * 15 * (float) gt.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch sb) {
            int padding = 2;

            if (!Placed) {
                sb.Draw(BlankPixel, DrawPosition + new Vector2(-padding), new Rectangle(Point.Zero, Block.BlockSize + new Point(padding * 2)), Color.White);
                //sb.Draw(BlankPixel, RealPosition, new Rectangle(Point.Zero, Block.BlockSize), new Color(Color, .9f));
            }
            
            sb.Draw(BlankPixel, DrawPosition, new Rectangle(Point.Zero, Block.BlockSize), Color);
            //sb.Draw(BlankPixel, Coordinates.ToVector2() * Block.Size.ToVector2(), new Rectangle(Point.Zero, Block.Size), Color.FromNonPremultiplied(255, 0, 0, 120));
        }

        public bool Placed { get; set; }

        public Point Coordinates { get; set; }
        public Point RealCoordinates {
            get {
                //Vector2 temp = new Vector2(((Mouse.Position.X - Block.Position.X) - (Mouse.Position.X - Block.Position.X) % Block.BlockSize.X) / Block.BlockSize.X, ((Mouse.Position.Y - Block.Position.Y) - (Mouse.Position.Y - Block.Position.Y) % Block.BlockSize.Y) / Block.BlockSize.Y).ToPoint() + Coordinates;

                return new Vector2(((Mouse.Position.X - Block.Position.X) - (Mouse.Position.X - Block.Position.X) % Block.BlockSize.X) / Block.BlockSize.X, ((Mouse.Position.Y - Block.Position.Y) - (Mouse.Position.Y - Block.Position.Y) % Block.BlockSize.Y) / Block.BlockSize.Y).ToPoint() + Coordinates;
            }
            set {
                
            }
        }

        public Vector2 Position { get; set; }
        public Vector2 DrawPosition { get; set; }
        public Vector2 RealPosition {
            get {
                return Block.Position + new Vector2(Block.Spacing + (Block.BlockSize.X + Block.Spacing) * RealCoordinates.X, Block.Spacing + (Block.BlockSize.Y + Block.Spacing) * RealCoordinates.Y);
            }
        }

        public Rectangle Hitbox {
            get { return new Rectangle(Position.ToPoint() + Coordinates * Block.BlockSize, Block.BlockSize); }
        }

        public Block Block { get; set; }

        public Color Color { get; set; }
    }
}
