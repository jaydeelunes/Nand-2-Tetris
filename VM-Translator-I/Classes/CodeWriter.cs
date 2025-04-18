namespace VM_Translator.Classes;

class CodeWriter: IDisposable
{
    private StreamWriter _writer;
    private string _fileName;
    private int _compCount = 0;

    public CodeWriter(string filePath)
    {
        _writer = new StreamWriter(filePath);
        _fileName = Path.GetFileNameWithoutExtension(filePath);
    }

    // Write the code for arithmetic and logic operations
    public void WriteArithmetic(string operation)
    {
        // Writes the VM command as a comment
        _writer.WriteLine($"// {operation}");
        string asm = String.Empty;

        // Calls function based on operation
        if (operation is "add" or "sub" or "and" or "or" or "not" or "neg") asm = Arithmetic.Operation(operation);
        else if (operation is "gt" or "lt" or "eq"){
            asm = Arithmetic.Comparison(operation, _compCount);
            _compCount++;
        }

        // Write line to output
        _writer.WriteLine(asm);
    }

    public void WritePushPop(string command, string segment, int index)
    {
        string commandText = command == "C_PUSH" ? "push" : "pop";
        string asm = String.Empty;

        if (segment is "local" or "argument" or "this" or "that" or "temp" or "static")
        {
            _writer.WriteLine($"// {commandText} {segment} {index}");

            if (command == "C_PUSH") asm = Push(segment, index);
            else if (command == "C_POP") asm = Pop(segment, index);
        }
        else if (segment is "constant")
        {
            _writer.WriteLine($"// push constant {index}");
            asm = string.Join("\n",
                $"@{index}",
                "D=A",
                "@SP",
                "A=M",
                "M=D",
                "@SP",
                "M=M+1");
        }
        else if (segment is "pointer")
        {
            string pointerAddress = index switch
            {
                0 => "THIS",
                1 => "THAT",
                _ => throw new InvalidOperationException("Pointer address does not exist")
            };

            if (command == "C_PUSH")
            {
                asm = string.Join("\n",
                    $"@{pointerAddress}",
                    "D=M",
                    "@SP",
                    "A=M",
                    "M=D",
                    "@SP",
                    "M=M+1");
            }
            else if (command == "C_POP")
            {
                asm = string.Join("\n",
                "@SP",
                "AM=M-1",
                "D=M",
                $"@{pointerAddress}",
                "M=D");
            }
        }
        _writer.WriteLine(asm);
    }

    public void Dispose()
    {
        _writer.Dispose();
    }

    private string BaseAddress(string segment, int index)
    {
        return segment switch
        {
            "local" => "LCL",
            "argument" => "ARG",
            "this" => "THIS",
            "that" =>"THAT",
            "temp" => "5",
            "static" => $"{_fileName}.{index}",
            _ => throw new InvalidOperationException("There is no segment given")
        };
    }

    private string Push(string segment, int index)
    {
        string register = segment is "temp" or "static" ? "A" : "M";

        return string.Join("\n",
            $"@{BaseAddress(segment, index)}",
            $"D={register}",
            $"@{index}",
            "A=D+A",
            "D=M",
            "@SP",
            "A=M",
            "M=D",
            "@SP",
            "M=M+1");
    }
    
    private string Pop(string segment, int index)
    {
        string register = segment is "temp" or "static" ? "A" : "M";

        return string.Join("\n",
            $"@{BaseAddress(segment, index)}",
            $"D={register}",
            $"@{index}",
            "D=D+A",
            "@R13",
            "M=D",
            "@SP",
            "AM=M-1",
            "D=M",
            "@R13",
            "A=M",
            "M=D");
    }
}