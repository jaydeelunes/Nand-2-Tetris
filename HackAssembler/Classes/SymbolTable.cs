namespace HackAssembler.Classes;

class SymbolTable
{
    private int VariableLocation = 16;
    private Dictionary<string, int> Table = new()
    {
            {"R0", 0},
            {"R1", 1},
            {"R2", 2},
            {"R3", 3},
            {"R4", 4},
            {"R5", 5},
            {"R6", 6},
            {"R7", 7},
            {"R8", 8},
            {"R9", 9},
            {"R10", 10},
            {"R11", 11},
            {"R12", 12},
            {"R13", 13},
            {"R14", 14},
            {"R15", 15},
            {"SCREEN", 16384},
            {"KBD", 24576},
            {"SP", 0},
            {"LCL", 1},
            {"ARG", 2},
            {"THIS", 3},
            {"THAT", 4},
    };

    // Store labels
    public void AddLabel(KeyValuePair<string, int> label)
    {
        this.Table.Add(label.Key, label.Value);
    }

    // Check if contains
    public bool Contains(string symbol)
    {
        return this.Table.ContainsKey(symbol);
    }

    // Get index
    public int Get(string symbol)
    {
        return this.Table[symbol];
    }

    public int CreateNewVariable(string symbol)
    {
        this.Table.Add(symbol, this.VariableLocation);
        this.VariableLocation++;
        return this.VariableLocation - 1;
    }

    public void PrintDictionary()
    {
        foreach (KeyValuePair<string, int> symbol in this.Table)
        {
            Console.WriteLine($"Key: {symbol.Key} | Value: {symbol.Value}");
        }
    }
}