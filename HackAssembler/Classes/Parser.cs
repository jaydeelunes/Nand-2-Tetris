namespace HackAssembler.Classes;

class Parser
{
    static public Dictionary<string, int> ParseLabels(string fileName)
    {
        // Initialize dictionary of labels with corresponding instruction number
        Dictionary<string, int> labels = new();

        // Open stream to read the .asm file
        using(StreamReader input = new StreamReader(fileName))
        {
            // Initialize first line and instruction number and read all the lines
            String? line = input.ReadLine();
            int instructionNumber = 0;
            while (line != null)
            {
                // Ignore empty lines and comments
                if (String.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("//"))
                {
                    line = input.ReadLine();
                    continue;
                }
                // If a line is a label, store it in the label dictionary
                else if (line.StartsWith("("))
                {
                    string label = line.Replace("(", "").Replace(")", "").Trim();
                    labels.Add(label, instructionNumber);
                }
                // If line is an insruction, increment the instruction count
                else
                {
                    instructionNumber++;
                }

                line = input.ReadLine();
            }
        }

        // Return all labels
        return labels;
    }

    static public List<string[]> ParseTokens(string fileName)
    {
        // Initialize list of instruction tokens
        List<string[]> parsed_file = new();

        // Open Stream to read the .asm file
        using (StreamReader input = new StreamReader(fileName))
        {
            // Read first line and read till the end of the file
            String? line = input.ReadLine();
            while (line != null)
            {
                // Ignore empty lines and comments
                if (String.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("//"))
                {
                    line = input.ReadLine();
                    continue;
                }
                else if (line.StartsWith("("))
                {

                }
                // Store tokens of A-command
                else if (line.Trim().StartsWith("@"))
                {
                    string[] tokens = {"@", line.Trim()[1..] };
                    parsed_file.Add(tokens);
                }
                // Store tokens of C-command
                else
                {
                    string command = String.Empty;

                    if (line.Contains("=") && line.Contains(";"))
                    {
                        command = "=;";
                    }
                    else if (line.Contains("="))
                    {
                        command = "=";
                    }
                    else if (line.Contains(";"))
                    {
                        command = ";";
                    }

                    string[] tokens = {command};
                    tokens = tokens.Concat(line.Split([';', '='], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)).ToArray();
                    parsed_file.Add(tokens);
                }

                line = input.ReadLine();
            }
        }
        return parsed_file;
    }
}