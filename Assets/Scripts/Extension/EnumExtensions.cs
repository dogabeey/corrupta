using System;
using System.Collections.Generic;
using System.Linq;

public static class FlagEnumExtensions
{
    /// <summary>
    /// Returns the flags set in 'value'.
    /// - includeZero: returns the zero-value name (e.g., None) if value == 0
    /// - includeComposite: also returns composite named flags (e.g., A|B) if fully present
    /// </summary>
    public static List<T> GetSelectedFlags<T>(this T value, bool includeZero = false, bool includeComposite = false)
        where T : Enum
    {
        ulong bits = Convert.ToUInt64(value);
        var list = new List<T>();

        foreach (T f in Enum.GetValues(typeof(T)))
        {
            ulong mask = Convert.ToUInt64(f);

            if (mask == 0UL)
            {
                if (includeZero && bits == 0UL)
                    list.Add(f);
                continue;
            }

            bool isSet = (bits & mask) == mask;
            if (!isSet) continue;

            // Filter out composites unless requested (keep only power-of-two masks)
            if (!includeComposite && (mask & (mask - 1)) != 0UL)
                continue;

            list.Add(f);
        }

        return list;
    }

    /// <summary> Shortcut for the common case: only single-bit flags. </summary>
    public static List<T> GetSingleBitFlags<T>(this T value) where T : Enum
        => value.GetSelectedFlags(includeZero: false, includeComposite: false);
}
