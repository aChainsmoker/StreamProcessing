using System.Diagnostics;

namespace StreamProcessing;

public class FileProcessorLogger
{
    private readonly string _fileLogPath;

    public FileProcessorLogger(string fileLogPath)
    {
        _fileLogPath = fileLogPath;
        InitializeLogger();
    }

    private void InitializeLogger()
    {
        if(File.Exists(_fileLogPath))
        {
            File.Delete(_fileLogPath);
        }
        var textWriterTraceListener = new TextWriterTraceListener(_fileLogPath)
        {
            TraceOutputOptions = TraceOptions.DateTime
        };

        Trace.Listeners.Add(textWriterTraceListener);
        Trace.AutoFlush = true;
    }

    public void LogInfo(string message)
    {
        Trace.TraceInformation(message);
    }
    
    public void LogWarning(string message)
    {
        Trace.TraceWarning(message);
    }
    
    public void LogError(string message)
    {
        Trace.TraceError(message);
    }
}