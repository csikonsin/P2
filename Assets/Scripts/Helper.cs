using System;
using UnityEngine;
public static class Helper   {

    public static Color HexToColor(string hex)
    {
        Color c = new Color();
        if(hex.IndexOf("#")!=0)
        {
            return new Color(1f,1f,1f,1f);
        }
        hex = hex.Substring(1);

        uint hexInt = Convert.ToUInt32(hex, 16);

        float r = (hexInt >> 16) & 255;
        float g = (hexInt >> 8) & 255;
        float b = hexInt & 255;

        c.r = r/255;
        c.g = g/255;
        c.b = b/255;
        c.a = 1;

        return c;
    }
}
