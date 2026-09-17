using System.Globalization;

namespace Portfolio.Web.Components;

/// <summary>
/// Builds the :root override block for the theme picker.
///
/// This lives in a .cs file rather than inside the .razor @code block on
/// purpose. Razor locates the end of a @code block by matching braces, and
/// CSS is full of braces, so building this string inside a component can
/// confuse the parser. Plain C# files have no such problem.
/// </summary>
public static class ThemeCssBuilder
{
    public static string Build(string pink, string purple, string blue)
    {
        return ":root {"
             + $" --accent-pink: {pink};"
             + $" --accent-purple: {purple};"
             + $" --accent-blue: {blue};"
             + $" --accent-pink-rgb: {ToRgb(pink)};"
             + $" --accent-purple-rgb: {ToRgb(purple)};"
             + $" --accent-blue-rgb: {ToRgb(blue)};"
             + " }";
    }

    /// <summary>
    /// "#3b82f6" becomes "59 130 246", the space separated form that
    /// rgb(var(--x) / 0.5) needs. CSS cannot pull channels out of a hex
    /// variable, so both forms have to be emitted.
    /// </summary>
    public static string ToRgb(string hex)
    {
        var h = (hex ?? "").TrimStart('#');

        if (h.Length != 6)
        {
            return "255 255 255";
        }

        if (!int.TryParse(h[..2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var r) ||
            !int.TryParse(h[2..4], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var g) ||
            !int.TryParse(h[4..6], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var b))
        {
            return "255 255 255";
        }

        return $"{r} {g} {b}";
    }
}
