# Final-VR

## Steps to build

1. Open command prompt/Terminal in a location where you want the VR project folder to reside.
2. `git clone https://github.com/raghavprasad13/Final-VR.git`
3. Open Unity Hub and make sure you are in the Projects tab. Click "Add" and choose the folder that was created as a result of step 1
4. Make sure you have Unity version **2020.1.10f1** installed. This can be installed from Unity Hub by going to `Installs > Add > 2020.1.10f1`
5. Click on `Final-VR` and wait for Unity to build the project

### Adjusting FicTrac settings

For now FicTrac is being started from within the VR application with the FicTrac binary itself residing outside the VR application. Thus, in order to start FicTrac we are using a `.bat` file which contains the command to start FicTrac. The C# files containing the code to start FicTrac are:

- `TrackBuilder.cs`: The `StartFictrac` function contains the code which points to the location of the `.bat` file
- `Constants.cs`: Contains a field called `FictracPort`. The value of this field should match the `out_port` (or `sock_port` in later versions of Fictrac) parameter value in the FicTrac config file

### Shutting down FicTrac

For now, Fictrac can be shut down from within the VR by hitting the `Q` key. However, this might not always work in case the FPS is too low (< 1). The steps to shut down FicTrac manually after stopping the VR in Unity are as follows:

- Open a command prompt as administrator and type in `netstat -ano | findstr <port_number>` where `<port_number>` is to be replaced with the port number being used for FicTrac.
- Note the PID associated with the processes that are displayed as a result of the previous step. The PID will be the number on the far right of each line.
- `taskkill /F /pid <process_id>` where `<process_id>` is the PID noted in the previous step
