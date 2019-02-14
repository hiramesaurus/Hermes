using System;
using UnityEngine;
using Hiramesaurus.Hermes.Localization;
using UnityEngine.Events;

namespace Hiramesaurus.Hermes.Dialog
{
    public class DialogDisplayer : MonoBehaviour
    {
        public LocalizedText TargetTextArea;

        public DialogAsset Dialog;

        public bool ShowFirstOnEnable = true;
        
        public bool AutoNext;
        public float Delay = 5;

        public UnityEvent DialogFinished;

        
        public void ShowNext ()
        {
            var isNext = Dialog.TryGetNextDialog (TargetTextArea.LocalizationKey, out var next);
            if (isNext)
                TargetTextArea.SetContentFromLocalization (next);

            if (AutoNext && isNext)
            {
                Invoke (nameof (ShowNext), Delay);
            }
            
            if (Dialog.IsLastDialog (TargetTextArea.LocalizationKey))
                DialogFinished.Invoke ();
        }

        public void ShowPrevious ()
        {
            throw new NotImplementedException (nameof (ShowPrevious));
        }

        private void OnEnable ()
        {
            if (ShowFirstOnEnable)
                TargetTextArea.SetContentFromLocalization (Dialog.GetDialogEntryPoint ());
            if (AutoNext)
                Invoke (nameof (ShowNext), ShowFirstOnEnable ? Delay : 0);
        }

        private void OnDisable ()
        {
            CancelInvoke ();
        }
    }

}