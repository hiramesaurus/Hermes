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

        public static string GetLocalizedString (string key)
        {
            if (LocalizedString.TryGetValue (key, out var value))
                return value;
            return $"Missing Localization: {key}.";
        }

        public static void LoadLocalizationForLanguage (string langKey)
        {
            LocalizedString.Clear ();

            var timer = new Stopwatch ();           
            timer.Start ();

            
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
        
        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            LoadLocalizationForLanguage ("en_US");                       
        }
    }
    
}