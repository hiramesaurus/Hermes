using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Hirame.Localizer.Editor
{
    [CustomEditor(typeof(LocalizedText))]
    public class LocalizedTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ()
        {
            EditorGUILayout.TextField ("Language Key", Localization.CurrentLanguageKey, EditorStyles.label);
            DrawPropertiesExcluding (serializedObject, "m_Script");
        }
    }

}