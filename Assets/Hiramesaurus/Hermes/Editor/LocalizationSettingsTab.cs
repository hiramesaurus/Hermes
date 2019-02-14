using UnityEditor;
using UnityEngine;

namespace Hiramesaurus.Hermes.Localization.Editor
{
    
    public static class LocalizationSettingsTab
    {
        // TODO:
        // Change path
        public const string k_MyCustomSettingsPath = "Assets/Hirame/Localizer/LocalizationSettings.asset";

        [SettingsProvider]
        private static SettingsProvider GetSettingsTab ()
        {
            var provider = new SettingsProvider("Project/Hirame", SettingsScope.Project)
            {
                // By default the last token of the path is used as display name if no label is provided.
                label = "Hirame",
                // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:
                guiHandler = OnGUI,

                // Populate the search keywords to enable smart search filtering and label highlighting:
                //keywords = new HashSet<string>(new[] { "Number", "Some String" })
            };

            return provider;
        }

        private static void OnGUI (string searchContext)
        {
            var settings = GetOrCreateSettings ();
            using (new EditorGUI.ChangeCheckScope ())
            {
                EditorGUILayout.TextField ("Default Language Key", settings.DefaultLanguageKey);
            }
        }
               

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        
        internal static LocalizationSettings GetOrCreateSettings ()
        {
            var settings = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(k_MyCustomSettingsPath);
            if (settings != null)
                return settings;
            
            settings = ScriptableObject.CreateInstance<LocalizationSettings>();
            settings.DefaultLanguageKey = "en_US";
            AssetDatabase.CreateAsset(settings, k_MyCustomSettingsPath);
            AssetDatabase.SaveAssets();
            
            return settings;
        }
    }

}