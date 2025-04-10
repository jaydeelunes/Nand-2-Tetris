using System.Net;

namespace VM_Translator.Classes;

public class Parser
{
    private IEnumerator<string> _lines;
    private string _currentCommand;

    public Parser(string filePath)
    {
        var lines = File.ReadLines(filePath)
            .Select(line => line.Split("//")[0].Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line));

        _lines = lines.GetEnumerator();
        _currentCommand = String.Empty;
    }

    public bool HasMoreCommands()
    {
        return _lines.MoveNext();
    }

    public void Advance()
    {
        _currentCommand = _lines.Current;
    }
    
    public string CommandType()
    {
        var command = _currentCommand.Split()[0];
        return command switch
        {
            "push" => "C_PUSH",
            "pop" => "C_POP",
            _ => "C_ARITHMETIC"
        };
    }

    public string Arg1()
    {
        // Throw error if Arg1 is called while command type is C_RETURN
        if (CommandType() == "C_RETURN") throw new InvalidOperationException("No Arg1 to reutrn");

        // Return the arithmatic operation if command type is C_ARITHMETIC
        if (CommandType() == "C_ARITHMETIC")
        {
            return _currentCommand.Split()[0];
        }
        // Return the first argument for other command types
        else
        {
            return _currentCommand.Split()[1];
        }
    }

    public int Arg2()
    {
        string[] validCommandTypes = ["C_PUSH", "C_POP", "C_FUNCTION", "C_CALL"];

        if (validCommandTypes.Contains(CommandType()))
        {
            return int.Parse(_currentCommand.Split()[2]);
        }
        else
        {
            throw new InvalidOperationException("No Arg2 to return");
        }
    }
}