using UnityEditor;
using UnityEngine;

namespace Hirame.Localizer.Editor
{
    [CustomEditor (typeof (LocalizationReference))]
    public class LocalizationReferenceEditor : UnityEditor.Editor
    {
        private LocalizationReference localization;

        private string localizationContent;
        private string supportedLanguages = "x of n";
        private Color supportColor = Color.red;
        private bool localizationFound;

        private bool editLanguageKey;

        private void OnEnable ()
        {
            localization = target as LocalizationReference;
            if (localization == null)
            {
                return;
            }

            localizationFound = Localization.TryGetLocalizedString (localization.ReferenceKey, out localizationContent);
        }

        public override void OnInspectorGUI ()
        {
            //GUILayout.Button ("Open Text Asset");

            using (new GUILayout.VerticalScope (EditorStyles.helpBox))
            {
                using (var changed = new EditorGUI.ChangeCheckScope ())
                {
                    DrawLangSelector ();
                    localization.ReferenceKey =
                        EditorGUILayout.DelayedTextField ("Reference Key", localization.ReferenceKey);

                    DrawLangSupport ();
                    
                    if (changed.changed)
                    {
                        OnEnable ();
                        serializedObject.ApplyModifiedProperties ();
                        EditorUtility.SetDirty (this);
                        return;
                    }
                }
            }

            using (new GUILayout.VerticalScope (EditorStyles.helpBox))
            {
                if (localizationFound)
                {
                    EditorGUILayout.LabelField ("Localization Content", EditorStyles.boldLabel);
                    EditorGUILayout.TextArea (localizationContent, GUILayout.Height (16 * 10));
                    
                    using (new GUILayout.HorizontalScope ())
                    {
                        GUILayout.Button ("Save");
                        GUILayout.Button ("Discard");
                    }
                }
                else
                {
                    var color = GUI.color;
                    GUI.color = Color.red;
                    EditorGUILayout.LabelField ("Localization Missing!");
                    GUI.color = color;
                }
            }
        }

        private void DrawLangSupport ()
        {
            using (new GUILayout.HorizontalScope ())
            {
                EditorGUILayout.PrefixLabel ("Supported Languages"); 

                var color = GUI.color;
                GUI.color = supportColor;
                EditorGUILayout.TextField (supportedLanguages, EditorStyles.label);
                GUI.color = color;
            }
        }
        

        private void DrawLangSelector ()
        {
            using (new GUILayout.HorizontalScope ())
            {
                if (editLanguageKey)
                {
                    localization.LanguageKey =
                        EditorGUILayout.DelayedTextField ("Language Key", localization.LanguageKey);
                }
                else
                {
                    EditorGUILayout.TextField ("Language Key", localization.LanguageKey, EditorStyles.label);
                    if (GUILayout.Button ("Switch", GUILayout.Width (60)))
                    {
                        editLanguageKey = !editLanguageKey;
                    }
                }
            }
        }
    }
}