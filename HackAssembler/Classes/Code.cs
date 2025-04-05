namespace HackAssembler.Classes;

class Code
{
    static public List<string> ConvertTokens(List<string[]> parsed_file, SymbolTable symbol_table)
    {
        // Initialize list of binary instructions
        List<string> binary_instructions = new();
        
        // Go over every instruction (collection of tokens)
        foreach (string[] tokens in parsed_file)
        {
            // Initialize the instruction values
            string compare = String.Empty;
            string destination = String.Empty;
            string jump = String.Empty;

            // If it is an A command
            if (tokens[0] == "@")
            {
                // Initialize 
                string binary = String.Empty;

                // If numerical -> Convert to binary
                if (int.TryParse(tokens[1], out _))
                {
                    binary = Convert.ToString(Convert.ToInt32(tokens[1], 10), 2).PadLeft(16, '0');
                }
                // If symbol in table -> Return and convert to binary
                else if (symbol_table.Contains(tokens[1]))
                {
                    binary = Convert.ToString(symbol_table.Get(tokens[1]), 2).PadLeft(16, '0');
                }
                // If symbol not in table -> Create variable
                else
                {
                    int new_variable = symbol_table.CreateNewVariable(tokens[1]);
                    binary = Convert.ToString(new_variable, 2).PadLeft(16, '0');
                }

                binary_instructions.Add(binary);
                Console.WriteLine($"{String.Join(", ", tokens)} | {binary}"); // DEBUG
                continue;
            }
            // If it is a command without JMP
            else if (tokens[0] == "=")
            {
                compare = tokens[2];
                destination = tokens[1];
                jump = "NULL";
            }
            // If it is a command without DST
            else if (tokens[0] == ";")
            {
                compare = tokens[1];
                destination = "NULL";
                jump = tokens[2];
            }
            // If it is a command with a DST and JMP
            else if (tokens[0] == ";=")
            {
                compare = tokens[2];
                destination = tokens[1];
                jump = tokens[3];
            }

            // Return binary instructions
            InstructionC instruction = new InstructionC(compare, destination, jump);
            binary_instructions.Add(instruction.GetBinary());
            Console.WriteLine($"{String.Join(", ", tokens)} | {instruction.GetBinary()}"); // DEBUG
        }
        return binary_instructions;
    }
}