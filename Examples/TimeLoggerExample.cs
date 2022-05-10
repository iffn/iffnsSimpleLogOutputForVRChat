
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Diagnostics;
using iffnsStuff.iffnsVRCStuff.DebugOutput;

namespace iffnsStuff.iffnsVRCStuff.DebugOutput.Examples
{
    public class TimeLoggerExample : UdonSharpBehaviour
    {
        [SerializeField] iffnsSimpleLogOutput logOutput;

        //Computer time
        System.DateTime offsetDateTime;

        //Stop watch
        Stopwatch stopwatchSinceStart;

        //My time
        double referenceTime;

        void Start()
        {
            //Computer time
            offsetDateTime = System.DateTime.UtcNow;
            
            //Stop watch
            stopwatchSinceStart = new Stopwatch();
            stopwatchSinceStart.Start();

            //VRC server time
            referenceTime = Networking.GetServerTimeInSeconds();

            //Titles
            logOutput.SetTitle(
                "Stopwatch time" + logOutput.GetTabSymbol() +
                "Computer time" + logOutput.GetTabSymbol() +
                "Unity time" + logOutput.GetTabSymbol() +
                "VRC Server time" + logOutput.GetTabSymbol() +
                "My time"
                );
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                double ComputerTime = (System.DateTime.UtcNow - offsetDateTime).TotalSeconds;
                double stopwatchTime = stopwatchSinceStart.Elapsed.TotalSeconds;
                float UnityTime = Time.time;
                double VRCServerTime = Networking.GetServerTimeInSeconds();
                double myTime = referenceTime + stopwatchSinceStart.Elapsed.TotalSeconds;

                logOutput.AddInput(
                    stopwatchTime.ToString("F6") + logOutput.GetTabSymbol() +
                    ComputerTime.ToString("F6") + logOutput.GetTabSymbol() +
                    UnityTime.ToString("F6") + logOutput.GetTabSymbol() +
                    VRCServerTime.ToString("F6") + logOutput.GetTabSymbol() +
                    myTime.ToString("F6")
                    );
            }
        }
    }
}