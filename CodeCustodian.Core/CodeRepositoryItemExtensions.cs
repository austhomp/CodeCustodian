namespace CodeCustodian.Core
{
    using System;
    using System.Linq;

    public static class CodeRepositoryItemExtensions
    {
        public static bool CanBeHandledBy<T>(this CodeRepositoryItem item, string baseName) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Generic Type 'T' must be an Enum");
            }

            if (item == null || string.IsNullOrEmpty(item.Type) || !item.Type.StartsWith(baseName))
            {
                return false;
            }

            var afterBaseName = item.Type.Substring(baseName.Length).TrimStart('.');
            return Enum.GetNames(typeof(T)).Any(name => name.ToUpperInvariant() == afterBaseName.ToUpperInvariant());
        }


        public static T? ParseAs<T>(this CodeRepositoryItem item, string baseName) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Generic Type 'T' must be an Enum");
            }

            if (item == null || string.IsNullOrEmpty(item.Type) || !item.Type.StartsWith(baseName))
            {
                return null;
            }

            var afterBaseName = item.Type.Substring(baseName.Length).TrimStart('.');

            if (Enum.GetNames(typeof(T)).Any(name => name.ToUpperInvariant() == afterBaseName.ToUpperInvariant()))
            {
                return (T)Enum.Parse(typeof(T), afterBaseName, true);
            }

            return null;
        }
    }
}