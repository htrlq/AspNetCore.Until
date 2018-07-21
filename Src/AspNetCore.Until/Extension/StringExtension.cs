namespace AspNetCore.Until.Extension
{
    public static class StringExtension
    {
        public static bool ToLowContains(this string src, string target)
        {
            return src.ToLower().Contains(target.ToLower());
        }
    }
}