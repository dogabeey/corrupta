using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Sirenix.OdinInspector;
using System.Linq;
internal static class ColorUtil
{
    public static float Distance(this Color a, Color b)
    {
        return Mathf.Sqrt((a.linear.r - b.linear.r) * (a.linear.r - b.linear.r) +
                          (a.linear.g - b.linear.g) * (a.linear.g - b.linear.g) +
                          (a.linear.b - b.linear.b) * (a.linear.b - b.linear.b));
    }
}