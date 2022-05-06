
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

        System.DateTime offsetDateTime;

        Stopwatch stopwatchSinceStart;

        double referenceTime;

        void Start()
        {
            referenceTime = Networking.GetServerTimeInSeconds();

            offsetDateTime = System.DateTime.UtcNow;

            stopwatchSinceStart = new Stopwatch();

            stopwatchSinceStart.Start();

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
                double stopwatchTime = stopwatchSinceStart.Elapsed.TotalSeconds;
                double ComputerTime = (System.DateTime.UtcNow - offsetDateTime).TotalSeconds;
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