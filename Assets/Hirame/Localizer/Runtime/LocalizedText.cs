using Hirame.Localizer;
using TMPro;
using UnityEngine;

public class LocalizedText : MonoBehaviour
{
    public string LocalizationKey;
    public TMP_Text Text;
    
    private void OnEnable ()
    {
        Localization.LocalizationUpdated += OnLocalizationUpdated;
        UpdateLocalization ();
    }

    private void OnDisable ()
    {
        Localization.LocalizationUpdated -= OnLocalizationUpdated;
    }

    private void OnLocalizationUpdated (string localizationKey)
    {
        UpdateLocalization ();
    }

    private void UpdateLocalization ()
    {
        Text.text = Localization.GetLocalizedString (LocalizationKey);
    }

    private void Reset ()
    {
        Text = GetComponent<TMP_Text> ();
    }
}
