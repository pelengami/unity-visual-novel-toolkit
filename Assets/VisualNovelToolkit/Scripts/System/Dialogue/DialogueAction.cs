using UnityEngine;

namespace Assets.Editor.System.Dialogue
{
    sealed class DialogueAction : IDialogueAction
    {
        public void Run()
        {
            //todo action
            Debug.Log(GetType().Name + " Action");
        }
    }
}