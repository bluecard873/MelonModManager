using UnityEngine;

namespace ModManager
{
    public static class WindowColor
    {
        public static Color ModListLayout = new Color32((byte) 18, (byte) 18, (byte) 18, (byte) 255);
        public static Color ModInfoLayout = new Color32((byte) 30, (byte) 30, (byte) 30, (byte) 255);
        public static Color RoundLayout = new Color32((byte) 45, (byte) 49, (byte) 53, (byte) 255);
        public static Color Invisible = new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 0);
        public static Color Translucent = new Color32((byte) 0, (byte) 0, (byte) 0, (byte) 150);
        public static Color ButtonColor = new Color32((byte)195, (byte) 143, (byte) 255, (byte) 255);
        
        public static string SelectText = "#bb86fc";
        public static string NotSelectText = "#969696";
        public static string OnlineText = "#43b581";
        public static string OfflineText = "#747f8d";
        public static string WarningText = "#faa61a";
        public static string ErrorText = "#f04747";
    }
}