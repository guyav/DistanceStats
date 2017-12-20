namespace DistanceCalculator.Common.ExtensionMethods
{
    public static class HebrewCharsExtensions
    {
        public static bool IsHebrew(this string str)
        {
            foreach (char c in str)
            {
                if (!(IsHebrewChar(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c)))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsHebrewChar(this char c)
        {
            foreach ((int, int) range in Map)
            {
                if (c >= range.Item1 && c <= range.Item2)
                {
                    return true;
                }
            }
            return false;
        }

        private static readonly (int, int)[] Map = new(int, int)[]
        {
            ( 0x05d0, 0x05ea ),
            ( 0x05b0, 0x05b9 ),
            ( 0x05bb, 0x05c4 ),
            ( 0xfb2a, 0xfb36 ),
            ( 0xfb38, 0xfb3c ),
            ( 0xfb3e, 0xfb3e ),
            ( 0xfb40, 0xfb41 ),
            ( 0xfb43, 0xfb44 ),
            ( 0xfb46, 0xfb4e ),
            ( 0x05f0, 0x05f2 ),
            ( 0xfb1f, 0xfb1f ),
            ( 0x05f3, 0x05f4 ),
            ( 0xfb20, 0xfb29 )
        };
    }
}