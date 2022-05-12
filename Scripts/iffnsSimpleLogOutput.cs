
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace iffnsStuff.iffnsVRCStuff.DebugOutput
{
    public class iffnsSimpleLogOutput : UdonSharpBehaviour
    {
        [Header("General key bindings:")]
        [SerializeField] KeyCode ClearKey = KeyCode.Delete;
        [SerializeField] KeyCode PauseKey = KeyCode.Pause;
        
        [Header("Key bindings for on screen prefab only:")]
        [SerializeField] KeyCode OnScreenOutputToggle = KeyCode.Home;
        [SerializeField] KeyCode OnScreenInfoToggle = KeyCode.End;

        [Header("Pre set assignments: (Do not change)")]
        [SerializeField] InputField OutputField;
        [SerializeField] Text LoggedLineCount;
        [SerializeField] Text HowToText;
        [SerializeField] bool OnScreen;
        [SerializeField] GameObject LogOutOfDateWarning;
        //[SerializeField] Canvas LinkedCanvas;
        [Header("For On screen display only: (Do not change)")]
        [SerializeField] GameObject OnScreenOutputDisplayHolder;
        [SerializeField] GameObject OnScreenActivator;

        const string newLine = "\n";
        int lineCount = 0;
        string title;
        string logText = "";
        bool paused = false;
        bool currentOutput = true;

        public void SetTitle(string title)
        {
            this.title = title;

            if(this.title.Length > 0)
            {
                this.title += newLine;
            }

            WriteOutput();
        }

        public void AddInput(string input)
        {
            logText += input + newLine;

            lineCount++;

            if (!paused)
            {
                WriteOutput();
            }
            else
            {
                WriteStatus();

                if (currentOutput)
                {
                    currentOutput = false;
                    LogOutOfDateWarning.SetActive(true);
                }
            }
        }

        public void SetPause(bool newState)
        {
            paused = newState;

            if (!paused)
            {
                WriteOutput();

                if (!currentOutput)
                {
                    currentOutput = true;
                    LogOutOfDateWarning.SetActive(false);
                }
            }
            else
            {
                WriteStatus();
            }
        }

        public string GetTabSymbol()
        {
            return "\t";
        }

        void WriteOutput()
        {
            OutputField.text = title + logText;

            WriteStatus();
        }

        void WriteStatus()
        {
            string pauseState;
            if (paused) pauseState = "Output paused";
            else pauseState = "Writing data when received, possibly laggy";

            LoggedLineCount.text = "Logged lines = " + lineCount
                    + newLine + "Logged symbols = " + logText.Length
                    + newLine + pauseState;
        }

        private void Start()
        {
            #if !UNITY_EDITOR
            if (OnScreen && Networking.LocalPlayer.IsUserInVR())
            {
                gameObject.SetActive(false);
            }
            #endif

            if (OnScreen)
            {
                HowToText.text = "On screen log output active";

                if(OnScreenInfoToggle != KeyCode.None) 
                    HowToText.text += newLine + "Press " + OnScreenInfoToggle.ToString() + " to toggle this message";

                if (OnScreenOutputToggle != KeyCode.None) 
                    HowToText.text += newLine + "Press " + OnScreenOutputToggle.ToString() + " to toggle the output display";

                HowToText.text += newLine + "Left click to select the text box";
                HowToText.text += newLine + "Press Ctrly A to select all if needed";
                
                #if !UNITY_EDITOR
                HowToText.text += newLine + "Hold Tab to select part of the text";
                #endif                
                
                if (ClearKey != KeyCode.None) 
                    HowToText.text += newLine + "Press " + ClearKey.ToString() + " to clear the log";
                
                if (PauseKey != KeyCode.None) 
                    HowToText.text += newLine + "Press " + PauseKey.ToString() + " to pause the output and reduce lag";
            }
            else
            {
                HowToText.text = "Log output";
                HowToText.text += newLine + "Press Ctrly A to select all if needed";
                
                if (ClearKey != KeyCode.None) 
                    HowToText.text += newLine + "Press " + ClearKey.ToString() + " to clear the log";
                
                if (PauseKey != KeyCode.None)
                    HowToText.text += newLine + "Press " + PauseKey.ToString() + " to pause the output and reduce lag";
            }
        }

        private void Update()
        {
            /*
            if (Input.GetKey(KeyCode.P))
            {
                Logger.AddInput("" + Time.time + "\t" + Time.deltaTime);
            }
            */

            if (Input.GetKeyDown(ClearKey))
            {
                logText = "";
                lineCount = 0;

                if(!paused) WriteOutput();
            }

            if (Input.GetKeyDown(PauseKey))
            {
                SetPause(!paused);
            }

            //if (LinkedCanvas.renderMode != RenderMode.WorldSpace) //Currently not supported by U#
            if (OnScreen)
            {
                if (Input.GetKeyDown(OnScreenOutputToggle))
                {
                    bool newState = !OnScreenOutputDisplayHolder.activeSelf;

                    OnScreenOutputDisplayHolder.SetActive(newState);
                    #if !UNITY_EDITOR
                    if (OnScreenActivator != null) OnScreenActivator.SetActive(newState);
                    #endif
                }

                if (Input.GetKeyDown(OnScreenInfoToggle))
                {
                    bool newState = !HowToText.gameObject.activeSelf;

                    HowToText.gameObject.SetActive(newState);
                    LoggedLineCount.gameObject.SetActive(newState);
                }
            }
        }
    }
}