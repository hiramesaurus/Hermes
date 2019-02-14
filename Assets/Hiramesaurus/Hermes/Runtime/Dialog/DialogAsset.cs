using System.Collections.Generic;
using UnityEngine;

namespace Hiramesaurus.Hermes.Dialog
{
    [CreateAssetMenu (menuName = "Hiramesaurus/Hermes/Dialog Asset")]
    public class DialogAsset : ScriptableObject
    {
        [SerializeField]
        private List<LocalizationReference> dialog;
        
        public LocalizationReference GetDialogEntryPoint ()
        {
            return dialog[0];
        }

        public bool IsLastDialog (LocalizationReference current)
        {
            return dialog[dialog.Count - 1].Equals (current);
        }
        
        public bool TryGetNextDialog (LocalizationReference current, out LocalizationReference next)
        {        
            var index = dialog.IndexOf (current);
            if (index == -1 || ++index >= dialog.Count)
            {
                Debug.Log (index.ToString());
                next = null;
                return false;
            }

            next = dialog[index];
            return true;
        }

        public class DialogBranch : ScriptableObject
        {
            public string Name;
            public LocalizationReference Content;
            public DialogBranch[] Branches;      
        }
    }

}