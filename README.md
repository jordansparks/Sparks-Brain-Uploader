# Sparks Brain Uploader

This will be a large complex software with a rich UI that will convert a connectome to a brain model ready for emulation. It will allow a large number of parameters, rules, and settings to be organized and referenced. There will then be a step that coallesces those complex parameters into the simpler parameters that will be used for actual emulation. Finally, it will be able to export that model to any of the major emulators.

## Requirements

- MySQL or MariaDB (pretty much any version will work)

## Initial Setup

1. Ensure MySQL or MariaDB is installed on your local machine.
2. MySQL username should be root with no password.
      - We will add support for other MySQL usernames and passwords later.
      - This software can share MySQL with other databases. All of our DBs will be prefixed with "sb_"
      - Our DBs will all force MyISAM table format for ease of backups and isolation.
3. Decide where you want your folder that stores all the large raw connectome data.
      - Default location will be C:\SparksBrainConnectomes\
      - But you will be able to set it on startup.

## Installation

- There is currently no way to install other than downloading source and building from source as described in the next sections.
- If there is any demand at all, we might eventually make a .zip available that contains the .exe and .dlls.
- We would put the .zip over at the right under Releases, Latest Build.
- (eventually)If you are downloading a new version, just extract right on top of the old version and choose "Replace Files in Destination".
- (eventually)Run SparksBrainUploader.exe

## Downloading Source

1. Option one is to download the source as a .zip
      - Use GitHub’s green Code → Download ZIP button
      - Extract it to someplace like C:\SparksBrainUploader\
2. Option two is to use Git to do the download
      - Install Git
      - I prefer to also install Tortoise Git
      - Visual Studio comes with Git built in, so that's another option
      - Clone the repository to a folder on your machine. Example: "C:\development\SparksBrainUploader\"

## Building From Source

1. Install Visual Studio 2026 (I use Professional Edition, but other editions will probably work)
2. Open the solution (.sln file) in Visual Studio
3. There is only one configuration: Debug / Main
4. The version is always 1.0.
5. There are no git branches. Just main.

## Usage

...coming soon...

## Development

- Jordan Sparks and my employees are the only contributors.

## License

- Apache License 2.0
- This allows anyone to use it freely for whatever they want.
