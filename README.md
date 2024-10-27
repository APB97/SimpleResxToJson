# Simple ResX to JSON

## Description

Command-line tool that simply converts a `*.resx` file OR all `*.resx` files in a directory and optionally its subdirectories into `*.json` equivalents and outputs them at desired location. Default options cause only values without `type` attribute specified to be included - to override this behavior, check __Usage__ section.

## Usage

### Parameters

`--input=<file|directory>`

__Required__ parameter: accepts path to a single file OR path to a directory containing `*.resx` files to be converted.

`--output=<directory>`

__Optional__ but __Recommended__ parameter: accepts path to a directory where `*.json` results will be written to. Otherwise current directory (working directory) will be used.

__Note__: Result(s) replace previous content of output `*.json` file(s) or create the files. Either make a backup or ensure there is no content in these files that cannot be lost.

`--recursive`, `-R`

__Optional__ parameter: When input is a directory then it causes application to search for `*.resx` files not only in top directory, but also in all subdirectories.

Note: In that case, output directory is used as base directory for results and each file is created at same path relative to input directory.

#### Example

When input is `/path/to/files`, and output directory is `/output/`, then file with path `/path/to/files/example/test.resx` will cause output file to be created at `/output/example/test.json`.

`--silent`

__Optional__ parameter: Make application stop output messages related to file processing from being written to console.

`--include-all`

__Optional__ parameter: When provided, this parameter groups data into `Strings` and `Files` properties. None of the files mentioned in a `*.rexx` file are read or checked for existence - instead, only data in a `*.resx` file is read and it's split into Value and Type properties with optional Encoding and Comment properties. Properties with `null` values are skipped during serialization and do not appear in output.

`--no-type-parsing`

__Optional__ parameter: When provided, all data is treated as strings, unless `--include-all` is specified - in that case, all data except `System.Resources.ResXFileRef, System.Windows.Forms` is treated as `Strings`. This parameter causes all data types other than `System.Resources.ResXFileRef, System.Windows.Forms` and without `type` attribute (`string`) to be considered as `Strings`.

### Single file conversion

#### Example

Executing compiled `SimpleResxToJson.CLI.exe` like this:

`SimpleResxToJson.CLI.exe --input=./Resources/resource.resx --output=./Json`

will create or replace file at `./Json/resource.json`.

### Directory conversion

#### Recursive example

<details>

Executing compiled `SimpleResxToJson.CLI.exe` like this:

`SimpleResxToJson.CLI.exe --recursive --input=./Resources --output=./Json/Resources`

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

</details>

#### Non-recursive example

<details>

Executing compiled `SimpleResxToJson.CLI.exe` like this:

`SimpleResxToJson.CLI.exe --recursive --input=./Resources --output=./Json/Resources`

when in `./Resources` exist given files:

- `Resources`
	- `resource.resx`
	- `resource.pl-PL.resx`
	- `NotIncludedByDefault`
		- `example.resx`

will create or replace files:

- `Json`
	- `Resources`
		- `resource.json`
		- `resource.pl-PL.json`

</details>
