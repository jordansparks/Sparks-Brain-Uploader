# Sparks Brain Uploader

This will be a large complex software with a rich UI that will convert a connectome to a brain model ready for emulation. It will allow a large number of parameters, rules, and settings to be organized and referenced. There will then be a step that coallesces those complex parameters into the simpler parameters that will be used for actual emulation. Finally, it will be able to export that model to any of the major emulators.

## Requirements

- MySQL or MariaDB (pretty much any version will work)
- VisualStudio 2026 (I use Professional Edition, but other editions will probably work)

## Initial Setup

1. Ensure MySQL or MariaDB is installed on your local machine.
2. MySQL username should be root with no password.
      We will add support for other MySQL usernames and passwords later.
      This software can share MySQL with other databases. All of our DBs will be prefixed with "sb_"
      Our DBs will all force MyISAM table format for ease of backups and isolation.
3. Clone the repository to a folder on your machine.
4. Decided where you want your folder that stores all the large raw connectome data.
      Default location is C:\SparksBrain\
      You will be able to change it on startup.
5. Open the solution (.sln file) in Visual Studio
6. ...more soon...

## Usage

...

## Development

- Jordan Sparks and my employees are the only contributors.
- There are no branches, just main.

## License

Apache License 2.0
This allows anyone to use it freely for whatever they want.
