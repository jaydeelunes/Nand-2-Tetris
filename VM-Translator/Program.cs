using VM_Translator.Classes;

class Program
{
    public static void Main(string[] args)
    {
        // Check if an argument was passed, if not -> Abort
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: dotnet run file.vm");
            Environment.Exit(1);
        }
        
        // Check if file extension is .vm, if not -> Abort
        string file_extension = Path.GetExtension(args[0]);
        if (file_extension != ".vm")
        {
            Console.WriteLine($"Error: '{args[0]}' is not end in '.vm'");
            Environment.Exit(2);
        }
        // Check if app was able to open file, if not -> Abort
        else if (!File.Exists(args[0]))
        {
            Console.WriteLine($"Error: Was not able to open '{args[0]}'");
            Environment.Exit(3);
        }

        // Read and parse provided .vm document
        var parser = new Parser(args[0]);

        // Translate every command to asm and write to .asm file
        using var coder = new Code(args[0].Split(".")[0] + "asm");
        while (parser.HasMoreCommands())
        {
            parser.Advance();

            var type = parser.CommandType();
            var arg1 = parser.Arg1();

            if (type == "C_ARITHMETIC")
            {
                coder.WriteArithmetic(arg1);
            }
            else if (type is "C_PUSH" or "C_POP" or "C_FUNCTION" or "C_CALL")
            {
                var arg2 = parser.Arg2();
                coder.WritePushPop(type, arg1, arg2);
            }
            else
            {
                throw new InvalidOperationException("Command type not implemented in the current version");
            }
        }
    }
}