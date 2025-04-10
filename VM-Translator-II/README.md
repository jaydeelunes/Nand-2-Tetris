# Project 8 - VM Translator pt. 2
In this project I will continue working on the VM Translator that I worked on in project 7.
The main features that will be added is the ability to write brancing and function commands to Hack assembly.

## Updates
- Handling a directory with multiple files as input in addition to a single file
- Parser can handle branching and function commands (goto, if-goto, label, call, function, return)
- CodeWriter class includes methods to write Hack Assembly for branching and function commands
    - writeInit()
    - writeLabel(string label)
    - writeGoto(string label)
    - writeIf(string label)
    - writeFunction(string functionName, int numVars)
    - writeCall(string functionName, int numArgs)
    - writeReturn()
