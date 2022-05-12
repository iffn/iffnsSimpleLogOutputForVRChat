using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace iffnsStuff.iffnsVRCStuff.DebugOutput.Helpers
{
    public class ActivatorPositioner : UdonSharpBehaviour
    {
        private void Update()
        {
            #if UNITY_EDITOR
            return;
            #endif

            transform.position = Networking.LocalPlayer.GetBonePosition(HumanBodyBones.Head) + Networking.LocalPlayer.GetBoneRotation(HumanBodyBones.Head) * Vector3.forward;
            transform.rotation = Networking.LocalPlayer.GetBoneRotation(HumanBodyBones.Head);
        }
    }
}