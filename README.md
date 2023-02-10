# ShortsMaker
A simple tool that generates a bunch of 50 second clips from one video. Easily create youtube shorts and tiktoks from longer videos. 

Give it a video file and amount of clips, and ShortsMaker generates the amount of clips at random times in the video.

## Disclaimer : You might still need to edit the video, sync audio etc. ShortsMaker just generates random sections of the video for you

# You need to install ffmpeg for this to work:

You can install ffmpeg and ffprobe on Windows using Chocolatey, a package manager for Windows. To install Chocolatey, follow these steps:

    Open a PowerShell window as an administrator
    Run the following command to install Chocolatey:

`Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))`

After installing Chocolatey, you can use the following command to install ffmpeg and ffprobe:

`choco install ffmpeg`

This will download and install the latest version of ffmpeg and ffprobe on your machine. 


# You need to add ffmpeg to your Path

Here are the steps to add the ffprobe executable to the PATH environment variable on Windows:

1.     Open the Start menu and search for "Environment Variables".
2.     Click on "Edit the system environment variables".
3.     In the System Properties window, click on the "Environment Variables" button.
4.     In the Environment Variables window, scroll down to the "System variables" section, and find the "Path" variable.
5.     Click on the "Edit" button next to the "Path" variable.
6.     In the Edit environment variable window, click on the "New" button and enter the path to the ffprobe executable. For example, if you installed ffmpeg using the choco install ffmpeg command, the path to the ffprobe executable may be C:\Program Files\ffmpeg\bin.
7.     Click "OK" to close all the windows and save your changes.
