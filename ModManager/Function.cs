﻿using UnityEngine;

namespace ModManager
{
    public static class Function
    {
        public static Texture2D makeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i) pix[i] = col;
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }


        public static Texture2D makeRectangle(int resolutionmultiplier, int width, int height, int borderThickness,
            int borderRadius, Color borderColor)
        {

            width = width * resolutionmultiplier;
            height = height * resolutionmultiplier;

            Texture2D texture = new Texture2D(width, height);
            Color[] color = new Color[width * height];

            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    color[x + width * y] = ColorBorder(x, y, width, height, borderThickness, borderRadius, borderColor);
                }
            }

            texture.SetPixels(color);
            texture.Apply();
            return texture;
        }

        private static Color ColorBorder(int x, int y, int width, int height, int borderThickness, int borderRadius,
            Color borderColor)
        {
            Rect internalRectangle = new Rect((borderThickness + borderRadius), (borderThickness + borderRadius),
                width - 2 * (borderThickness + borderRadius), height - 2 * (borderThickness + borderRadius));

            Vector2 point = new Vector2(x, y);
            if (internalRectangle.Contains(point)) return borderColor;

            Vector2 origin = Vector2.zero;

            if (x < borderThickness + borderRadius)
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(borderRadius + borderThickness, borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(borderRadius + borderThickness, height - (borderRadius + borderThickness));
                else
                    origin = new Vector2(borderRadius + borderThickness, y);
            }
            else if (x > width - (borderRadius + borderThickness))
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(width - (borderRadius + borderThickness), borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(width - (borderRadius + borderThickness),
                        height - (borderRadius + borderThickness));
                else
                    origin = new Vector2(width - (borderRadius + borderThickness), y);
            }
            else
            {
                if (y < borderRadius + borderThickness)
                    origin = new Vector2(x, borderRadius + borderThickness);
                else if (y > height - (borderRadius + borderThickness))
                    origin = new Vector2(x, height - (borderRadius + borderThickness));
            }

            if (!origin.Equals(Vector2.zero))
            {
                float distance = Vector2.Distance(point, origin);

                if (distance > borderRadius + borderThickness + 1)
                {
                    return Color.clear;
                }
                else if (distance > borderRadius + 1)
                {
                    return borderColor;
                }
            }

            return borderColor;
        }
    }
}