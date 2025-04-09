using HackAssembler.Classes;

class Program
{
    static void Main(string[] args)
    {
        // Check if file exists
        if (!File.Exists(args[0]))
        {
            Console.WriteLine($"Error: Could not find '{args[0]}'");
            System.Environment.Exit(1);
        }

        // Save extension
        string file_extension = Path.GetExtension(args[0]);
        
        // Check if the file extension is correct
        if (file_extension != ".asm")
        {
            Console.WriteLine($"Error: '{args[0]}' does not have the '.asm' file extension");
            System.Environment.Exit(1);
        }

        // Create symbol table
        SymbolTable symbol_table = new();

        // First pass for labels and store in symbol table
        Dictionary<string, int> labels = Parser.ParseLabels(args[0]);
        foreach (KeyValuePair<string, int> label in labels)
        {
            symbol_table.AddLabel(label);
        }

        // Parse file and return tokens
        List<string[]> tokens = Parser.ParseTokens(args[0]);

        // Convert tokens to binary
        List<string> binary_instructions = Code.ConvertTokens(tokens, symbol_table);

        // Write binary instruction to .hack file
        using (StreamWriter output = new(args[0].Replace(file_extension, ".hack"), false))
        {
            foreach (string binary_instruction in binary_instructions)
            {
                output.WriteLine(binary_instruction);
            }
        }
    }
}