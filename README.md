# iffnsSimpleLogOutputForVRChat
### Overview
- Simple log output for use with UdonSharp.
- Provides the ability to select and copy the output text.
- When using tabs in the output, the correct pasting into a spreadsheed programs should work directly.
- With in World and On screen prefab.
- On screen output automatically disabled for VR users.
- Example compares different ways of measuring the time ingame.
  - Analysis: https://docs.google.com/spreadsheets/d/1TpoI61EexUdCbio0k_yPTfiYiW2Z2r3_bQr2vgMqF2k

## How to use
### Requirements
- Current Unity version for VRChat: https://docs.vrchat.com/docs/current-unity-version
- VRChat World SDK3: https://vrchat.com/home/download
- UdonSharp: https://github.com/vrchat-community/UdonSharp

### Integration without Submodules
To maintain compatability with other projects, please put everything into ```/Assets/iffnsStuff/iffnsSimpleLogOutputForVRChat``` 

### Git Submodule integration
Add this submodule with the following git command (Assuming the root of your Git project is the Unity project folder)
```
git submodule add https://github.com/iffn/iffnsSimpleLogOutputForVRChat.git Assets/iffnsStuff/iffnsSimpleLogOutputForVRChat
```
