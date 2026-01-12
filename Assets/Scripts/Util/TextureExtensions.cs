using System.Collections.Generic;
using UnityEngine;

public static class TextureExtensions
{
    public static List<Color32> GetUniqueColors(this Texture2D texture)
    {
        var pixels = texture.GetPixels32();
        var set = new HashSet<Color32>(pixels);
        return new List<Color32>(set);
    }
}
