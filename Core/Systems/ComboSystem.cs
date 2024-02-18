using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Chat;
using Terraria.Chat.Commands;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.UI.Chat;

namespace cherryblossomtest.Core.Systems
{


    public class ComboSystem : ModSystem
    {
        private Dictionary<string, Action> comboDatabase;
        private Queue<ModKeybind> keyQueue;
        private TimeSpan keySequenceTimeout;
        private DateTime lastKeyPressTime;
        private const int MaxSequenceLength = 10;

        // Define ModHotKeys
        public static ModKeybind Chord1;
        public static ModKeybind Chord2;
        public static ModKeybind Chord3;

        public override void Load()
        {
            Chord1 = KeybindLoader.RegisterKeybind(Mod, "Chord Key 1", Keys.Z);
            Chord2 = KeybindLoader.RegisterKeybind(Mod, "Chord Key 2", Keys.X);
            Chord3 = KeybindLoader.RegisterKeybind(Mod, "Chord Key 3", Keys.C);

            comboDatabase = new Dictionary<string, Action>();
            keyQueue = new Queue<ModKeybind>();
            keySequenceTimeout = TimeSpan.FromMilliseconds(500); // Adjust timeout as needed
        }

        public override void Unload()
        {
            Chord1 = null;
            Chord2 = null;
            Chord3 = null;
        }

        public void Update()
        {
            if (Chord1.JustPressed)
            {
                EnqueueKey(Chord1);
                Main.NewText("Button1 pressed");
            }
            if (Chord2.JustPressed)
            {
                EnqueueKey(Chord2);
                Main.NewText("Button2 pressed");
            }
            if (Chord3.JustPressed)
            {
                EnqueueKey(Chord3);
                Main.NewText("Button3 pressed");
            }

            string sequence = GetCurrentKeySequence();
            if (sequence != null && comboDatabase.ContainsKey(sequence))
            {
                comboDatabase[sequence].Invoke();
                keyQueue.Clear();
            }
        }

        private void EnqueueKey(ModKeybind key)
        {
            TimeSpan timeSinceLastKeyPress = DateTime.Now - lastKeyPressTime;

            if (timeSinceLastKeyPress > keySequenceTimeout)
            {
                keyQueue.Clear();
            }

            keyQueue.Enqueue(key);
            lastKeyPressTime = DateTime.Now;

            if (keyQueue.Count > MaxSequenceLength)
            {
                keyQueue.Dequeue();
            }

            Console.WriteLine($"Enqueued key: {key.DisplayName.Value}. Current sequence: {GetCurrentKeySequence()}");
        }

        private string GetCurrentKeySequence()
        {
            List<string> keyNames = new List<string>();
            foreach (var key in keyQueue)
            {
                keyNames.Add(key.DisplayName.Value);
            }
            return string.Join("-", keyNames);
        }

        public void RegisterCombo(string keySequence, Action action)
        {
            if (!comboDatabase.ContainsKey(keySequence))
            {
                comboDatabase.Add(keySequence, action);
            }

            Console.WriteLine($"Registered combo: {keySequence}");
        }

        // Example of how to register a combo
        public void RegisterSampleCombo()
        {
            RegisterCombo("Combo Key Z-Combo Key X-Combo Key C", () =>
            {
                // Define combo action here
            });
        }
    }

}