using UnityEngine;

namespace MyProject.Utils
{
    public static class ColorStore
    {
        public static readonly Color TransparentColorRed = new Color(1f, 0f, 0f, 0.3f);
        public static readonly Color TransparentColorGreen = new Color(0f, 1f, 0f, 0.3f);
        public static readonly Color TransparentColorBlue = new Color(0f, 0f, 1f, 0.3f);
        public static readonly Color TransparentColorYellow = new Color(1f, 1f, 0f, 0.3f);
        public static readonly Color TransparentColorCyan = new Color(0f, 1f, 1f, 0.3f);
        public static readonly Color TransparentColorMagenta = new Color(1f, 0f, 1f, 0.3f);
        public static readonly Color TransparentColorOrange = new Color(1f, 0.5f, 0f, 0.3f);
        public static readonly Color TransparentColorPurple = new Color(0.5f, 0f, 0.5f, 0.3f);
        public static readonly Color TransparentColorPink = new Color(1f, 0.75f, 0.8f, 0.3f);
        public static readonly Color TransparentColorBrown = new Color(0.6f, 0.3f, 0.1f, 0.5f);

        private static readonly Color[] colors = new Color[10]
        {
            TransparentColorRed,
            TransparentColorGreen,
            TransparentColorBlue,
            TransparentColorYellow,
            TransparentColorCyan,
            TransparentColorMagenta,
            TransparentColorOrange,
            TransparentColorPurple,
            TransparentColorPink,
            TransparentColorBrown
        };
        public static Color GetColor(TypeColor color)
        {
            return colors[(int)color];
        }
    }

    public enum TypeColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Cyan,
        Magenta,
        Orange,
        Purple,
        Pink,
        Brown
    }
}
