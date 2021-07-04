# CI-Depth and AudioMod
A Color Image Depth Converter and an Audio bit rate converter

[![CodeFactor](https://www.codefactor.io/repository/github/owentheprogrammer/ia-depthconverter/badge)](https://www.codefactor.io/repository/github/owentheprogrammer/ia-depthconverter)

# Description
This is a two part project, AudioMod is for converting audio bitrate and CIDepth.java is for converting image color palettes.
---
# Project Video from [My Youtube Channel](https://www.youtube.com/channel/UCBN7kAz6Rbso4d97u3BYK2w):
<a href="http://www.youtube.com/watch?feature=player_embedded&v=FmKso8zsRJo" target="_blank"><img src="http://img.youtube.com/vi/FmKso8zsRJo/0.jpg" 
alt="Making Bit Wars Accurately" width="640" height="480" border="10" /></a>
---

# Example videos (Was in Repo)
These are the example videos. to save on GitHub storage, I have moved these videos into youtube as an unlisted video:
1. [Bit Depth Example Video](https://youtu.be/MA2SIkZfr7k)
2. [Total Step Count Example Video](https://youtu.be/T0QXu6qF100)
---
# Dependencies for CI-Depth (Image)
1. [Open JDK 11](https://github.com/AdoptOpenJDK/openjdk11-binaries/releases/download/jdk-11.0.10+9/OpenJDK11U-jdk_x64_windows_hotspot_11.0.10_9.msi)
2. [Java](https://www.java.com/en/)

# Dependencies for CI-Depth (AudioMod)
1. [Visual Studio: Universal Windows Platform Development](https://docs.microsoft.com/en-us/windows/uwp/get-started/create-a-hello-world-app-xaml-universal)

---
# Usage CI-Depth (Image)
1. Clone this repository to somewhere, the folder that holds this readme.md file will be refered as `installationPath`
2. Using Visual Studio Code, open the `installationPath/src` folder, then open CIDepth.java
3. Visual Studio may prompt you to install Java, if this is the case then the installs linked above will be there as well.
4. If it didnt prompt you to install anything, make sure you have [This Java extension](https://marketplace.visualstudio.com/items?itemName=vscjava.vscode-java-pack)
5. Just to be sure, restart your computer before proceeding
6. In visual studio with CIDepth.java open, open a new terminal
7. after using cd to redirect your terminal into the `installationPath`, run the command `cls; java ".\src\CIDepth.java"`
8. if the console prints "Saved file to data/Output.png", then its working
9. ChangeImageCP("Path to Input Folder, Path to Output Folder, Value, True: Value -> Depth, False: Value -> Steps);

# Usage CI-Depth (AudioMod)
1. Clone this repository to somewhere, the folder that holds this readme.md file will be refered as `installationPath`
2. Using Visual Studio (not VSCode) Open the `installationPath/AudioMod/AudioMod.sln`
3. The project build is under `installationPath/AudioMod/bin/Debug/netcoreapp3.1/`
4. reference data is under `installationPath/AudioMod/bin/Debug/netcoreapp3.1/Data/`

---
# CI-Depth (AudioMod) Notes:
1. *.WAV is the only supported file type
2. all metadata in audio file should be cleared before trying to use this project (the header length will be really long)
3. remember to change the sample count for the audio manager constructor
4. **REMEMBER TO CHANGE THE SAMPLE COUNT FOR THE AUDIO MANAGER CLASS CONSTRUCTOR! (I forget all the time)**
5. if you export to "WAV Unsigned 8 bit" then AudioManager.BitsPerSample should be 8 and IsSigned should be false
6. if you export to "WAV Signed 16-24 bit" then AudioManager.BitsPerSample should be 16-24 and IsSigned should be true
7. The Assertions will tell you is wrong and how wrong
---
# Update notes:
* 2021-03-17:
  - I have moved the example videos to youtube to save on github repo space:
  - Bit Depth Example: https://youtu.be/MA2SIkZfr7k
  - Total Step Count Example: https://youtu.be/T0QXu6qF100
  - AudioMod/bin/Debug/* has been deleted
  - if you want to rebuild the project, you can build it yourself with visual studio
