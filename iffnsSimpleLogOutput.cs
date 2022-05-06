
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace iffnsStuff.iffnsVRCStuff.DebugOutput
{
    public class iffnsSimpleLogOutput : UdonSharpBehaviour
    {
        [SerializeField] InputField field;
        [SerializeField] bool EnableDeleteKey = true;

        const string newLine = "\n";

        string title;

        public void SetTitle(string title)
        {
            this.title = title;
            field.text = title + newLine;
        }

        public void AddInput(string input)
        {
            field.text += input + newLine;
        }

        public string GetTabSymbol()
        {
            return "\t";
        }

        private void Update()
        {
            /*
            if (Input.GetKey(KeyCode.P))
            {
                Logger.AddInput("" + Time.time + "\t" + Time.deltaTime);
            }
            */

            if (EnableDeleteKey && Input.GetKey(KeyCode.Delete))
            {
                field.text = title + newLine;
            }
        }
    }
}