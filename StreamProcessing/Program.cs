namespace StreamProcessing;

class Program
{
    static void Main(string[] args)
    {
        FileProcessor processor = new FileProcessor(new FileProcessorLogger("log.txt"));
        processor.Process("input.txt", "output.txt");
    }
}