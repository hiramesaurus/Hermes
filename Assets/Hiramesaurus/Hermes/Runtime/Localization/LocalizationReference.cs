using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hiramesaurus.Hermes
{
    [CreateAssetMenu (menuName = "Hiramesaurus/Hermes/Localization Reference")]
    public class LocalizationReference : ScriptableObject
    {
        public string LanguageKey;
        public string ReferenceKey;
        
        public bool IsLoaded { get; private set; }
    }
}