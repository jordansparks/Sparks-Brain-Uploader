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
3. Decided where you want your folder that stores all the large raw connectome data.
      - Default location will be C:\SparksBrain\
      - But you will be able to change it on startup.

## Installation

- All installation is done via Git
- 
- Download SparksBrainUploader-v0.1.zip
- Do not use GitHub’s green Code → Download ZIP button, because that downloads the source code
- Extract it to someplace like C:\SparksBrainUploader\
- If you are downloading a new version, just extract right on top of the old version and choose "Replace Files in Destination".
- Run SparksBrainUploader.exe

## Building From Source

(most users would not do this and would follow the Installation instructions above instead)
- This gives access to the most recent 

1. Install Visual Studio 2026 (I use Professional Edition, but other editions will probably work)
2. I prefer Tortoise Git instead of using the Visual Studio Git integration or Git command line.
      - If you wish to use Tortoise Git, install Git first
      - Then install Tortoise Git. 
3. Clone the repository to a folder on your machine.
      - I used "C:\SparksBrainUploader\" for the repository location on my machine. You might try that.
4. Open the solution (.sln file) in Visual Studio
5. ...more soon...

## Usage

...

## Development

- Jordan Sparks and my employees are the only contributors.
- There are no branches, just main.

## License

- Apache License 2.0
- This allows anyone to use it freely for whatever they want.
