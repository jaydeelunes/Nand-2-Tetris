namespace VM_Translator.Classes;

class Code: IDisposable
{
    private StreamWriter _writer;

    public Code(string filePath)
    {
        _writer = new StreamWriter(filePath);
    }

    public void WriteArithmetic(string operation)
    {
        
    }

    public void WritePushPop(string command, string segment, int index)
    {

    }

    public void Dispose()
    {
        _writer.Dispose();
    }
}