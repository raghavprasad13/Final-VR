# Final-VR

- [Final-VR](#final-vr)
  - [Getting started](#getting-started)
  - [Steps to build](#steps-to-build)
  - [Running the project](#running-the-project)
  - [Fictrac](#fictrac)
    - [Adjusting Fictrac settings](#adjusting-fictrac-settings)
    - [Shutting down Fictrac](#shutting-down-fictrac)
      - [Windows](#windows)
      - [macOS](#macos)
  - [Neuralynx](#neuralynx)
  - [For contributors](#for-contributors)

## Getting started

Before building and running the project, there are a few prerequisites that need to be in place.

- `Unity 2020.1.10.f1`: Download [Unity Hub](https://unity3d.com/get-unity/download) and sign up for a personal license. Install Unity version `2020.1.10.f1` by going to `Installs > Add > 2020.1.10f1`. If this version of Unity is not visible in Unity Hub, visit the [Unity Downloads archive](https://unity3d.com/get-unity/download/archive) and choose to install the required version of Unity using the `Unity Hub` link for it. Next, choose the following modules for installation:
  - Mac Build Support (IL2CPP)
  - WebGL Build Support
  - Windows Build Support (Mono)
  - Visual Studio (optional, download only if you don't have this installed on your system already)

## Steps to build

1. Open command prompt/Terminal in a location where you want the VR project folder to reside.
2. `git clone https://github.com/raghavprasad13/Final-VR.git`
3. Open Unity Hub and make sure you are in the Projects tab. Click "Add" and choose the folder that was created as a result of step 1
4. Make sure you have Unity version **2020.1.10f1** installed. This can be installed from Unity Hub by going to `Installs > Add > 2020.1.10f1`
5. Click on `Final-VR` and wait for Unity to build the project

## Running the project

1. Press the play button
2. Choose a Track (`.track`) file [choose either `random_5.track` or `lrt_strobe.track` for now]. The track files reside in the project folder at `Assets/Resources/Tracks`
3. Move the avatar around using the arrow keys or `WASD`
4. Press `Q` to quit

## Fictrac

For now, Fictrac has to be downloaded and installed separately. In the future, we will bundle Fictrac with the rest of the VR code.

### Adjusting Fictrac settings

Earlier, Fictrac was being started from within the VR application with the Fictrac binary itself residing outside the VR application. Thus, in order to start Fictrac for Windows, we used a `.bat` file which contains the command to start Fictrac. For Mac/Linux, passing the command as a string within the code suffices. The command to initiate Fictrac is

    <path to Fictrac binary/executable> <path to Fictrac config file>

However, since Fictrac was being installed externally, the command to initiate Fictrac needed to be modified to the local installation.

Now, Fictrac is shipped with the VR application and resides in the project folder at `Assets\StreamingAssets\fictrac`. Thus, there is no need to modify the Fictrac inititation command to the local installation since now there is no need for a separate installation.

This modification from the earlier version has only been implemented for macOS/Linux systems. Windows systems will have to modify the `.bat` file as follows:

- **For Windows**: Modify `fictrac_starter.bat` which can be found in the project folder at `Assets\StreamingAssets\fictrac_starter.bat`

Additionally, the main C# file concerned with Fictrac handling is `FictracController.cs`. Also `Constants.cs` contains a field called `FictracPort`. The value of this field should match the `out_port` (or `sock_port` in later versions of Fictrac) parameter value in the Fictrac config file

### Shutting down Fictrac

For now, Fictrac can be shut down from within the VR by hitting the `Q` key. However, this might not always work in case the FPS is too low (< 1). The steps to shut down Fictrac manually after stopping the VR in Unity are as follows:

#### Windows

- Open a command prompt as administrator and type in `netstat -ano | findstr <port_number>` where `<port_number>` is to be replaced with the port number being used for Fictrac.
- Note the PID associated with the processes that are displayed as a result of the previous step. The PID will be the number on the far right of each line.
- `taskkill /F /pid <process_id>` where `<process_id>` is the PID noted in the previous step

#### macOS

- Open Activity Monitor and find the `fictrac` process
- Double-click the `fictrac` process to open it up in a separate dialog box
- Press the `Quit` button

## Neuralynx

Neuralynx is the electrophysiological recording hardware apparatus which is going to be controlled using Arduino generated TTL pulses. The Unity VR project sends start and stop signals to the Arduino. The code to control the Arduino is housed in `NeuralynxController.cs`  
  
The code requires the name of the Arduino Serial port beforehand. Currently this is being hardcoded in the constant `ArduinoPort` in `Constants.cs`. This hardcoded value needs to be updated according to the local configuration. The name of the serial port can be ascertained by checking `Tools > Port` in the Arduino IDE. This piece of code can later be modified to read the Serial port name and the baud rate from either a track file or from the OS.

## For contributors

**Only push the `Assets` and `ProjectSettings` folders (and `README.md`) of the Unity project to GitHub.** These folders are sufficient to recreate the project. If you try to push other parts of the project it may be rejected due to Github's 100MB file size limit. Make sure to use [GitHub Flow](https://guides.github.com/introduction/flow/index.html) to make changes to the repository.
