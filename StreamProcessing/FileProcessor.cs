using System.Globalization;

namespace StreamProcessing;

public class FileProcessor
{
    private readonly FileProcessorLogger? _logger;

    public FileProcessor()
    {
        
    }
    
    public FileProcessor(FileProcessorLogger logger)
    {
        _logger = logger;
    }
    
    public void Process(string inputFilePath, string outputFilePath)
    {
        try
        {
            var lines = ReadAndProcess(inputFilePath);
            WriteToFile(outputFilePath, lines);
        }
        catch (FileNotFoundException e)
        {
            var errorString = $"input file {inputFilePath} was not found";
            _logger?.LogError(errorString);
            throw new FileNotFoundException(errorString, e);
        }
        catch (IOException e)
        {
            var errorString = $"An IO error occured while processing file: {e.Message}";
            _logger?.LogError(errorString);
            throw new IOException(errorString, e);
        }
    }

    private List<string> ReadAndProcess(string inputFilePath)
    {
        using var fileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var streamReader = new StreamReader(fileStream);
        _logger?.LogInfo($"Reading and processing file: {inputFilePath}");
        
        var lines = new List<string>();
        while (streamReader.ReadLine() is { } line)
        {
            if (!line.Contains("skip", StringComparison.OrdinalIgnoreCase))
            {
                lines.Add(line.ToUpper());
                continue;
            }
            _logger?.LogInfo($"Skipping line: {line}");
        }
        lines.Sort();
        
        return lines;
    }

    private void WriteToFile(string outputFilePath,List<string> lines)
    {
        using var fileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
        using var streamWriter = new StreamWriter(fileStream);
        _logger?.LogInfo($"Writing to file: {outputFilePath}");
        
        foreach (var line in lines)
        {
            streamWriter.WriteLine(line);
        }
    }
}