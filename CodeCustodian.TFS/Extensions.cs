namespace CodeCustodian.TFS
{
    using System;

    public static class Extensions
    {
        public static string[] SplitLines(this string source)
        {
            return source.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        }
    }
}