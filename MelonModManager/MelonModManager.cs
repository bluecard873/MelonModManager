using UnityEngine;
using MelonLoader;
using MelonModManager.Core;

namespace MelonModManager {
    /// <summary>
    /// The main runner of the <see cref="MelonModManager"/> plugin.
    /// </summary>
    public class MelonModManager : MelonPlugin {
        public static MelonModManager Instance { get; private set; }
        
        /// <summary>
        /// The Settings instance of <see cref="MelonModManagerSettings"/>.
        /// </summary>
        public MelonModManagerSettings Settings => MelonModManagerSettings.Instance;

        /// <summary>
        /// Instance of <see cref="ModWindow"/>.
        /// </summary>
        public ModWindow Window => ModWindow.Instance;

        private bool _windowsEnabled = false;

        /// <summary>
        /// The enabled status of windows.
        /// </summary>
        public bool WindowsEnabled {
            get => _windowsEnabled;
            set {
                _windowsEnabled = value;
                ModWindow.Instance.gameObject.SetActive(value);
            }
        }

        /// <summary>
        /// Prepare to load mod informations and setup GUI.
        /// </summary>
        public override void OnApplicationStart() {
            Instance = this;
            // Load pre-saved preferences
            MelonModManagerSettings.Load();
        }

        /// <summary>
        /// Toggle all installed mods.
        /// </summary>
        public override void OnApplicationLateStart() {
            MelonLogger.Msg("Late start");
            // Create GameObject and append Component
            var windowObject = new GameObject("ModManager_Window");
            windowObject.AddComponent<ModWindow>();

            // Disable UI if user prefers to
            if (!Settings.OpenGUIOnStartup) {
                windowObject.SetActive(false);
            }
        }

        /// <summary>
        /// Save all prefs and quit the application.
        /// </summary>
        public override void OnApplicationQuit() {
            MelonModManagerSettings.Save();
        }

        /// <summary>
        /// Toggle <see cref="ModWindow"/> compoenent with specific keybind.
        /// </summary>
        public override void OnLateUpdate() {
            // Close window by specific keycombo
            if (Settings.OpenGUIKeyCombo.CheckKeyDown()) {
                WindowsEnabled = !WindowsEnabled;
            }

            // Close window on hitting escape while not writing
            if (WindowsEnabled && Input.GetKeyDown(KeyCode.Escape) && !Window.IsWriting) {
                WindowsEnabled = false;
            }
        }
    }
}