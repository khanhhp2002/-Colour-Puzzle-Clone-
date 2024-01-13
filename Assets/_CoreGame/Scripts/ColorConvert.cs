using UnityEngine;

public static class ColorConvert
{
    public static int ToHUE(this Color32 color32)
    {
        int max = Mathf.Max(color32.r, color32.g, color32.b);
        int min = Mathf.Min(color32.r, color32.g, color32.b);
        if (max == min)
            return 0;

        float hue = 0;
        if (max == color32.r)
            hue = (color32.g - color32.b) / (float)(max - min);
        else if (max == color32.g)
            hue = 2 + (color32.b - color32.r) / (float)(max - min);
        else
            hue = 4 + (color32.r - color32.g) / (float)(max - min);
        return Mathf.RoundToInt(hue) * 60;
    }
}
