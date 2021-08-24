using MelonLoader;
using MelonModManager.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace MelonModManager.Core
{
    public class ModWindow : MonoBehaviour {
        private static ModWindow instance;

        /// <summary>
        /// Instance of <see cref="ModWindow"/>.
        /// </summary>
        public static ModWindow Instance {
            get => instance;
        }

        /// <summary>
        /// Whether the player is writing in internal UI.
        /// </summary>
        public bool IsWriting { get; private set; }

        /// <summary>
        /// Window's container.
        /// </summary>
        private Rect WindowRect = new Rect();

        /// <summary>
        /// Currently selected mod.
        /// </summary>
        private MelonMod SelectedMod;

        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            // Set window size
            WindowRect.width = (float) (Screen.width / 1.4);
            WindowRect.height = (float) (Screen.height / 1.4);
            WindowRect.x = Screen.width / 2 - WindowRect.width / 2;
            WindowRect.y = Screen.height / 2 - WindowRect.height / 2;  
        }

        private void Update() {
            // TODO: Write code to display the mod window
        }
    }
}
