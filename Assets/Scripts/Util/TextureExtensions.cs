using System.Collections.Generic;
using UnityEngine;

public static class TextureExtensions
{
    public static List<Color> GetUniqueColors(this Texture2D texture)
    {
        var pixels = texture.GetPixels();
        var set = new HashSet<Color>(pixels);
        return new List<Color>(set);
    }
}
