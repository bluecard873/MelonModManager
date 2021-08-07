using UnityEngine;

namespace MelonModManager.Core {
    /// <summary>
    /// Class for storing <see cref="ModWindow"/>'s UI Styles.
    /// </summary>
    public static class ModWindowStyles {
        /// <summary>
        /// .
        /// </summary>
        public static class Colors {
            /**
             * Note that Color32's constructor recieves R, G, B, A
             * as parameters and their values should be in range of 0 to 255.
             */
            public static Color ModListLayout = new Color32(18, 18, 18, 255);
            public static Color ModInfoLayout = new Color32(30, 30, 30, 255);
            public static Color RoundLayout = new Color32(45, 49, 53, 255);
            public static Color Invisible = new Color32(0, 0, 0, 0);
            public static Color Translucent = new Color32(0, 0, 0, 150);
            public static Color ButtonColor = new Color32(195, 143, 255, 255);

            // TODO: Replace hex string to actual color instances and replace all <color> tags
            public static string SelectText = "#bb86fc";
            public static string NotSelectText = "#969696";
            public static string OnlineText = "#43b581";
            public static string OfflineText = "#747f8d";
            public static string WarningText = "#faa61a";
            public static string ErrorText = "#f04747";
        }
    }
}
