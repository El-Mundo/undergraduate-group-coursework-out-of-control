using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ControlDisabler
    {
        public readonly static string[] KEY_NAMES = { "Jump", "Left", "Right", "Run", "Up" };
        public Dictionary<string, bool> keysEnabled;

        public ControlDisabler()
        {
            keysEnabled = new Dictionary<string, bool>();
            foreach(string name in KEY_NAMES)
            {
                keysEnabled.Add(name, true);
            }
        }

        /// <summary>
        /// Get whether a key is usable.
        /// </summary>
        /// <param name="button">The key's name (as Unity's Input Manager Button)</param>
        /// <returns>Whether or not the key is usable.</returns>
        public bool KeyUsable(string button)
        {
            bool output = false;
            bool hasKey = keysEnabled.TryGetValue(button, out output);
            if (!hasKey)
            {
                return true;
            }
            else
            {
                return output;
            }
        }

        public void SetKey(string button, bool value)
        {
            if (keysEnabled.ContainsKey(button))
            {
                keysEnabled[button] = value;
                UnityEngine.Debug.Log("KEY " + button + (value ? " ENABLED" : " DISABLED"));
            }
        }

    }
}
