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

        private Rect windowRect = new Rect();

        private void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            // Set window size
            windowRect.width = (float) (Screen.width / 1.4);
            windowRect.height = (float) (Screen.height / 1.4);
            windowRect.x = Screen.width / 2 - windowRect.width / 2;
            windowRect.y = Screen.height / 2 - windowRect.height / 2;  
        }

        private void Update() {
            
        }

        private void OnGUI() {
            
        }
    }
}
