namespace HackAssembler.Classes;

class InstructionC
{
    private static readonly Dictionary<string, string> _CompareTable = new()
    {
        {"0",   "101010"},
        {"1",   "111111"},
        {"-1",  "111010"},
        {"D",   "001100"},
        {"A",   "110000"},
        {"!D",  "001101"},
        {"!A",  "110001"},
        {"-D",  "001111"},
        {"-A",  "110011"},
        {"D+1", "011111"},
        {"A+1", "110111"},
        {"D-1", "001110"},
        {"A-1", "110010"},
        {"D+A", "000010"},
        {"D-A", "010011"},
        {"A-D", "000111"},
        {"D&A", "000000"},
        {"D|A", "010101"},
    };
    private static readonly Dictionary<string, string> _DestinationTable = new()
    {
        {"NULL", "000"},
        {"M", "001"},
        {"D", "010"},
        {"MD", "011"},
        {"A", "100"},
        {"AM", "101"},
        {"AD", "110"},
        {"ADM", "111"}
    };
    private static readonly Dictionary<string, string> _JumpTable = new()
    {
        {"NULL", "000"},
        {"JGT", "001"},
        {"JEQ", "010"},
        {"JGE", "011"},
        {"JLT", "100"},
        {"JNE", "101"},
        {"JLE", "110"},
        {"JMP", "111"}
    };
    public string A { get; set; }
    public string Compare { get; set; }
    public string Destination { get; set; }
    public string Jump { get; set; }

    public InstructionC(string compare, string destination, string jump)
    {
        this.A = compare.Contains('M') ? "1" : "0";
        this.Compare = _CompareTable[compare.Replace('M', 'A')];
        this.Destination = _DestinationTable[destination];
        this.Jump = _JumpTable[jump];
    }
    public string GetBinary()
    {
        return "111" + A + Compare + Destination + Jump;
    }
}