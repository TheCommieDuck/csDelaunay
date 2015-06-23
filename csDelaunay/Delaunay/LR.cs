namespace csDelaunay {
    public static class LR
    {
        public const byte Left = 0;
        public const byte Right = 1;

        public static byte Other(byte curr)
        {
            return (byte)(curr == 1 ? 0x0 : 0x1);
        }
    }
}
