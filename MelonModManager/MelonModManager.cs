using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using MelonModManager.Core;

namespace MelonModManager {
    /// <summary>
    /// The main runner of the MelonModManager plugin.
    /// </summary>
    public class MelonModManager : MelonPlugin {
        /// <summary>
        /// Instance of <see cref="ModWindow"/>.
        /// </summary>
        public ModWindow Window => ModWindow.Instance;

        /// <summary>
        /// <see cref="ModWindow"/>'s GameObject.
        /// </summary>
        public GameObject WindowObject { get; private set; }

        private bool _windowsEnabled;

        /// <summary>
        /// .
        /// </summary>
        public bool WindowsEnabled {
            get => _windowsEnabled;
            set {
                _windowsEnabled = value;
                WindowObject.SetActive(value);
            }
        }

        /// <summary>
        /// Prepare to load mod informations and setup GUI.
        /// </summary>
        public override void OnApplicationStart() {
            WindowObject = new GameObject("ModManager_Window");
            WindowObject.AddComponent<ModWindow>();

            
        }

        /// <summary>
        /// Toggle all installed mods.
        /// </summary>
        public override void OnApplicationLateStart() {

        }

        /// <summary>
        /// Save all prefs and quit the application.
        /// </summary>
        public override void OnApplicationQuit() {
            MelonPreferences
        }

        /// <summary>
        /// Toggle <see cref="ModWindow"/> compoenent with specific keybind.
        /// </summary>
        public override void OnLateUpdate() {
            // TODO: Add a if statement to allow custom keybind from being used to toggle
            if (Input.GetKeyDown(KeyCode.F10) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) {
                WindowsEnabled = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && WindowsEnabled) {
                WindowsEnabled = false;
            }
        }
    }
}