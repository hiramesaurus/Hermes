using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Localizer
{
    [CreateAssetMenu (menuName = "Hirame/Localizer/Localization Reference")]
    public class LocalizationReference : ScriptableObject
    {
        public string LanguageKey;
        public string ReferenceKey;
        
        public bool IsLoaded { get; private set; }
    }
}