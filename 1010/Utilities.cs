﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace _1010 {
    public static class Utilities {
        public static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        static char[] consonantsList = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        static char[] vowelsList = { 'a', 'e', 'i', 'o', 'u' };
        static string[] methods = { "V1c1v1c1v1c1v1", "C1c1v1c1v1", "C1v1c2v2", "C1v1c1v1c1v1", "V1c1v2", "C1v1c2v2c1", "C1v1c2v1c2v1c1", "C1c1v1c1v2c1", "C1v1c2v1c2", "C1c1v1c2 C1v2c1v1c1" };

        static Dictionary<char, int> consonants = new Dictionary<char, int>() {
            { 'b', 9 },
            { 'c', 9 },
            { 'd', 9 },
            { 'f', 9 },
            { 'g', 9 },
            { 'h', 9 },
            { 'j', 8 },
            { 'k', 8 },
            { 'l', 9 },
            { 'm', 9 },
            { 'n', 9 },
            { 'p', 9 },
            { 'q', 5 },
            { 'r', 8 },
            { 's', 9 },
            { 't', 9 },
            { 'v', 4 },
            { 'w', 4 },
            { 'x', 3 },
            { 'y', 7 },
            { 'z', 5 }
        };

        static Dictionary<char, int> consonantsReset;
        public static IEnumerable<string> SplitToLines(string stringToSplit, int maximumLineLength) {
            var words = stringToSplit.Split(' ').Concat(new[] { "" });
            return
                words
                    .Skip(1)
                    .Aggregate(
                        words.Take(1).ToList(),
                        (a, w) => {
                            var last = a.Last();
                            while (last.Length > maximumLineLength) {
                                a[a.Count() - 1] = last.Substring(0, maximumLineLength);
                                last = last.Substring(maximumLineLength);
                                a.Add(last);
                            }
                            var test = last + " " + w;
                            if (test.Length > maximumLineLength) {
                                a.Add(w);
                            } else {
                                a[a.Count() - 1] = test;
                            }
                            return a;
                        });
        }

        public static Int32 Next(Int32 min, Int32 max) {
            uint scale = uint.MaxValue;

            while (scale == uint.MaxValue) {
                byte[] four_bytes = new byte[4];
                rand.GetBytes(four_bytes);

                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            return (int) (min + (max - min) * (scale / (double) uint.MaxValue));
        }
        public static float Next(float min, float max) {
            uint scale = uint.MaxValue;

            while (scale == uint.MaxValue) {
                byte[] four_bytes = new byte[4];
                rand.GetBytes(four_bytes);

                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            return (min + (max - min) * (scale / (float) uint.MaxValue));
        }

        public static float Distance(Point p1, Point p2) {
            return (float) Math.Abs(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
        }
        public static float Distance(Vector2 p1, Vector2 p2) {
            return (float) Math.Abs(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
        }

        public static string RandomName() {
            string name = "", method = methods[Next(0, methods.Length)];
            int t;

            for (int i = 0; i < method.Length; i++) {
                if (method[i] == ' ') {
                    name += " ";
                } else {
                    for (int j = 0; i + 1 < method.Length && Int32.TryParse(method[i + 1].ToString(), out t) && j < Convert.ToInt32(method[i + 1].ToString()); j++) {
                        if (method[i].ToString().ToUpper() == "C") {
                            while (true) {
                                char temp = consonantsList[Next(0, consonants.Count)];

                                try {
                                    if (consonants[temp] > Next(0, 10)) {
                                        name += method[i].ToString().ToUpper() == method[i].ToString() ? temp.ToString().ToUpper() : temp.ToString();

                                        break;
                                    }
                                } catch (Exception) { }
                            }
                        } else if (method[i].ToString().ToUpper() == "V") {
                            name += method[i].ToString().ToUpper() == method[i].ToString() ? vowelsList[Next(0, vowelsList.Length)].ToString().ToUpper() : vowelsList[Next(0, vowelsList.Length)].ToString();
                        }
                    }
                }
            }

            return name;
        }

        public static Point RandomPoint(Rectangle rect, int diameter) {
            return new Point(Next(0, rect.Width), Next(0, rect.Height));
        }
        public static Vector2 RandomVector2(Rectangle rect, int diameter) {
            if (diameter > rect.Width || diameter > rect.Height)
                return Vector2.Zero;
            else {
                return new Vector2(Next(0, rect.Width - diameter), Next(0, rect.Height - diameter));
            }
        }

        public static float CorrectAngle(float angle) {
            float corrected;

            if (angle < -Math.PI)
                corrected = (float) Math.PI - Math.Abs((float) Math.PI + angle);
            else if (angle > Math.PI)
                corrected = (float) -Math.PI + Math.Abs((float) Math.PI - angle);
            else
                return angle;

            return corrected;
        }

        public static Color RandomColor() {
            int red = Next(0, 255), green = Next(0, 255), blue = Next(0, 255), correction = 30, range = 100;

            while (Math.Abs(red - green) < range)
                green = Next(0, 255);

            while (Math.Abs(green - blue) < range)
                blue = Next(0, 255);

            red += (255 - red) / correction;
            green += (255 - green) / correction;
            blue += (255 - blue) / correction;

            return Color.FromNonPremultiplied(red, green, blue, 255);
        }
        public static Color RandomStaticColor() {
            return Colors[Next(0, Colors.Count)];
        }

        public static List<Color> Colors {
            get {
                return new List<Color>() {
                    Color.FromNonPremultiplied(255, 20, 70, 255),
                    Color.FromNonPremultiplied(50, 200, 50, 255),
                    Color.FromNonPremultiplied(120, 120, 255, 255),
                };
            }
        }
    }
}
