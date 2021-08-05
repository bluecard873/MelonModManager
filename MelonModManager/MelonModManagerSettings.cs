using System.Collections.Generic;
using MelonLoader;

namespace MelonModManager.Core {
    /// <summary>
    /// .
    /// </summary>
    public class MelonModManagerSettings {
        /// <summary>
        /// .
        /// </summary>
        public Dictionary<string, bool> EnabledMods = new Dictionary<string, bool>();

        private static MelonModManagerSettings _instance;

        /// <summary>
        /// .
        /// </summary>
        public static MelonModManagerSettings Instance => _instance;

        private static readonly MelonPreferences_Category PrefCategory = MelonPreferences.CreateCategory("MelonModManagerSettings");
        private static readonly MelonPreferences_Entry<MelonModManagerSettings> PrefEntry = PrefCategory.CreateEntry("MelonModManagerSettings", new MelonModManagerSettings());

        public static void Load() {
            PrefCategory.LoadFromFile();
            _instance = PrefEntry.Value;
        }

        public static void Save(MelonModManagerSettings instance) {
            PrefEntry.Value = instance;
            PrefCategory.SaveToFile();
        }
    }
}
