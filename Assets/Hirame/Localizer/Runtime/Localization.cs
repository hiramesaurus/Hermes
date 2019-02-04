using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Hirame.Localizer
{
    public static class Localization
    {
        public static event System.Action<string> LocalizationUpdated;
        
        private static Dictionary<string, string> LocalizedString = new Dictionary<string, string> ();
        // TODO:
        // Add support for localized sprites.
        //private static Dictionary<string, Sprite> LocalizedSprites = new Dictionary<string, Sprite> ();

        public const string DefaultLangKey = "en_US";
        
        public static string CurrentLanguageKey { get; private set; } = "None";

        private static bool LocalizationLoaded;
        
        public static string GetLocalizedString (string key)
        {
            if (LocalizedString.TryGetValue (key, out var value))
                return value;
            return $"Missing Localization: {key}.";
        }

        public static bool TryGetLocalizedString (string key, out string result)
        {
            if (!LocalizationLoaded)
                LoadLocalizationForLanguage (DefaultLangKey);
            
            return LocalizedString.TryGetValue (key, out result);
        }

        public static void LoadLocalizationForLanguage (string langKey)
        {
            // TODO:
            // Most of this method could be moved to its own thread?
            
            if (CurrentLanguageKey.Equals (langKey))
                return;

            LocalizationLoaded = true;
            LocalizedString.Clear ();

            var timer = new Stopwatch ();           
            timer.Start ();
            CurrentLanguageKey = langKey;
            
            var path = Path.Combine (Application.streamingAssetsPath, "Localization");
            Debug.Log (path);

            var dirs = Directory.EnumerateDirectories (path);
            var langDir = dirs.First (s => s.EndsWith (langKey));

            if (string.IsNullOrEmpty (langDir))
            {
                Debug.LogError ($"Lang not found! {langKey}.");
                return;
            }

            var files = Directory.GetFiles (langDir, "*.txt");

            foreach (var f in files)
            {
                var length = Path.GetFileName (f).Length - 4;
                var key = Path.GetFileName (f).Substring (0, length);

                var content = File.ReadAllText (f);
                LocalizedString.Add (key, content);
            }
            
            timer.Stop ();
            Debug.Log ($"Loaded localization in {timer.ElapsedMilliseconds.ToString ()}ms.");

        }
        
        #if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
        #endif
        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            LoadLocalizationForLanguage ("en_US");                       
        }
    }
    
}