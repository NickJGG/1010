using Microsoft.Xna.Framework;

namespace _1010 {
    public class BlockType {
        public BlockType(int[,] values, Color color) {
            Values = values;

            Color = color;
        }

        public int[,] Values { get; set; }

        public Color Color { get; set; } = Utilities.RandomStaticColor();
    }
}
