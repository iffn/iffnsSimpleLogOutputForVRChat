
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace iffnsStuff.iffnsVRCStuff.DebugOutput
{
    public class iffnsSimpleLogOutput : UdonSharpBehaviour
    {
        [SerializeField] InputField OutputField;
        [SerializeField] bool EnableDeleteKey = true;
        [SerializeField] Text LoggedLineCount;
        [SerializeField] bool OnScreen;
        [Header("For On screen display only:")]
        [SerializeField] GameObject HowToText;
        [SerializeField] GameObject OnScreenActivator;

        const string newLine = "\n";
        int lineCount = 0;
        string title;

        public void SetTitle(string title)
        {
            this.title = title;
            OutputField.text = title + newLine;
        }

        public void AddInput(string input)
        {
            OutputField.text += input + newLine;
            lineCount++;
            LoggedLineCount.text = "Logged lines = " + lineCount;
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

            if (EnableDeleteKey && Input.GetKeyDown(KeyCode.Delete))
            {
                OutputField.text = title + newLine;
                lineCount = 0;
                LoggedLineCount.text = "Logged lines = " + lineCount;
            }

            if (OnScreen)
            {
                if (EnableDeleteKey && Input.GetKeyDown(KeyCode.End))
                {
                    bool newState = !HowToText.gameObject.activeSelf;

                    HowToText.gameObject.SetActive(newState);
                    LoggedLineCount.gameObject.SetActive(newState);

                }

                if (Input.GetKeyDown(KeyCode.Home))
                {
                    bool newState = !OutputField.gameObject.activeSelf;

                    OutputField.gameObject.SetActive(newState);
                    if (OnScreenActivator != null) OnScreenActivator.SetActive(newState);
                }
            }
        }
    }
}