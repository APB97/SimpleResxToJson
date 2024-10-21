# Simple Resx to JSON

## Description

Command-line tool that simply converts a `*.resx` file OR all `*.resx` files in a directory and its subdirectories into `*.json` equivalents and outputs them at desired location.

## Usage

### Parameters

`--input=<file|directory>`

__Required__ parameter: accepts path to a single file OR path to a directory containing `*.resx` files.

`--output=<directory>`

__Optional__ but __Recommended__ parameter: accepts path to a directory where `*.json` results will be written to. Otherwise current directory (working directory) will be used.

__Note__: Result(s) replace previous content of output `*.json` file(s) or create the files. Either make a backup or ensure there is no content in these files that cannot be lost.

Note: In case of input being a directory, output directory is used as base directory for results and each file is created at same path relative to input directory.

#### Example

When input is `/path/to/files`, and output directory is `/output/`, then file with path `/path/to/files/example/test.resx` will cause output file to be created at `/output/example/test.json`.

### Single file conversion

#### Example

Executing compiled `SimpleResxToJson.CLI.exe` like this:

`SimpleResxToJson.CLI.exe --input=./Resources/resource.resx --output=./Json`

will create or replace file at `./Json/resource.json`.

### Directory conversion

Executing compiled `SimpleResxToJson.CLI.exe` like this:

`SimpleResxToJson.CLI.exe --input=./Resources --output=./Json/Resources`

when in `./Resources` exist given files:

- `Resources`
	- `resource.resx`
	- `Example`
		- `example.resx`

will create or replace files:

- `Json`
	- `Resources`
		- `resource.json`
		- `Example`
			- `example.json`
