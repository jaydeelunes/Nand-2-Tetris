namespace VM_Translator.Classes;

class Arithmetic
{
    // Return the Hack Machine Language commands to do a non-comparison operation
    public static string Operation(string operation)
    {
        string operationCommand = operation switch
        {
            "add" => "D+M",
            "sub" => "M-D",
            "and" => "D&M",
            "or" => "D|M",
            "neg" => "-M",
            "not" => "!M",
            _ => throw new InvalidOperationException("Operation does not exist")
        };

        return string.Join("\n",
        GetArguments(operation),
        $"D={operationCommand}",
        "@SP",
        "A=M",
        "M=D",
        "@SP",
        "M=M+1");
    }

    // Returns the Hack Machine Language commands to do a comparison operation
    public static string Comparison(string operation, int suffix)
    {
        string jumpToCorrectCondition = operation switch
        {
            "gt" => "JGT",
            "lt" => "JLT",
            "eq" => "JEQ",
            _ => throw new InvalidOperationException("Operation does not exist")
        };
    
        return string.Join("\n",
            GetArguments(operation),
            "D=M-D",
            $"@TRUE{suffix}",
            $"D;{jumpToCorrectCondition}",
            "@SP",
            "A=M",
            "M=0",
            $"@FALSE{suffix}",
            "0;JMP",
            $"(TRUE{suffix})",
            "@SP",
            "A=M",
            "M=-1",
            $"(FALSE{suffix})",
            "@SP",
            "M=M+1");
    }

    // Returns the Hack Machine Language commands to either pop 1 or 2 arguments of the stack for the operation
    private static string GetArguments(string operation)
    {
        string arguments = string.Join("\n",
            "@SP",
            "AM=M-1");

        if (operation != "neg" && operation != "not")
        {
            arguments = string.Join("\n",
                arguments,
                "D=M",
                "@SP",
                "AM=M-1");
        }

        return arguments;
    }
}