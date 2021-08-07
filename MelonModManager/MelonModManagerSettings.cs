using System.Collections.Generic;
using MelonLoader;
using MelonModManager.Util;

namespace MelonModManager.Core {
    /// <summary>
    /// Class for storing <see cref="MelonModManager"/>'s Settings.
    /// </summary>
    public class MelonModManagerSettings {
        /// <summary>
        /// Saved status of mods being enabled.
        /// </summary>
        public Dictionary<string, bool> EnabledMods = new Dictionary<string, bool>();

        /// <summary>
        /// Key Combo for opening <see cref="ModWindow"/>.
        /// </summary>
        public KeyCombo OpenGUIKeyCombo = new KeyCombo(UnityEngine.KeyCode.F10, true);

        /// <summary>
        /// Whether to open the UI automatically after the game is loaded.
        /// </summary>
        public bool OpenGUIOnStartup = true;

        private static MelonModManagerSettings _instance;

        /// <summary>
        /// Instance of <see cref="MelonModManagerSettings"/>.
        /// </summary>
        public static MelonModManagerSettings Instance => _instance;

        private static readonly MelonPreferences_Category PrefCategory = MelonPreferences.CreateCategory("MelonModManagerSettings");
        private static readonly MelonPreferences_Entry<MelonModManagerSettings> PrefEntry = PrefCategory.CreateEntry("MelonModManagerSettings", new MelonModManagerSettings());

        public static void Load() {
            PrefCategory.LoadFromFile();
            _instance = PrefEntry.Value;
        }

        public static void Save(MelonModManagerSettings instance = null) {
            PrefEntry.Value = instance ?? _instance;
            PrefCategory.SaveToFile();
        }
    }
}
