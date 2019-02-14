using Hiramesaurus.Hermes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Hiramesaurus.Hermes.Localization
{
    [ExecuteAlways]
    public class LocalizedText : MonoBehaviour
    {
        [FormerlySerializedAs ("LocalizationKey")] [SerializeField]
        private LocalizationReference localizationKey;
        public TMP_Text Text;

        public LocalizationReference LocalizationKey => localizationKey;

        public void SetContentFromLocalization (LocalizationReference localization)
        {
            localizationKey = localization;
            UpdateLocalization ();
        }
        
        private void OnEnable ()
        {
            Localization.LocalizationUpdated += OnLocalizationUpdated;
            UpdateLocalization ();
        }

        private void OnDisable ()
        {
            Localization.LocalizationUpdated -= OnLocalizationUpdated;
        }

        // TODO:
        // Remove parameter?
        private void OnLocalizationUpdated (string localizationKey)
        {
            UpdateLocalization ();
        }

        private void UpdateLocalization ()
        {
            if (localizationKey != null)
                Text.text = Localization.GetLocalizedString (localizationKey.ReferenceKey);
            else
                Text.text = string.Empty;
        }

        private void Reset ()
        {
            Text = GetComponent<TMP_Text> ();
        }
    }
}