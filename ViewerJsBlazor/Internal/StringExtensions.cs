using System.Linq;

namespace ViewerJsBlazor.Internal
{
    internal static class StringExtensions
    {
        internal static string ToCamelCase(this string name)
        {
            name ??= "";
            var parts = name.Trim().Split('_', '-')
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (parts.Length == 0) return "";
            if (parts.Length == 1)
            {
                return parts[0][0].ToString().ToLower() + parts[0][1..];
            }
            return ToCamelCase(parts[0]) + string.Join("", parts[1..].Select(x => ToPascalCase(x)));
        }

        internal static string ToPascalCase(this string name)
        {
            name ??= "";
            var parts = name.Trim().Split('_', '-')
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (parts.Length == 0) return "";
            if (parts.Length == 1)
            {
                return parts[0][0].ToString().ToUpper() + parts[0][1..];
            }
            return string.Join("", parts.Select(x => ToPascalCase(x)));
        }
    }
}
