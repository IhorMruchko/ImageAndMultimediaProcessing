using ImageAndMultimediaProcessing.Lib.Extensions;
using System;
using System.IO;
using System.Text;

namespace ImageAndMultimediaProcessing.Lib.Helpers.Time;

public class Logger
{
    private const string LOG_FORMAT = "Called: {0}\nResult: {1}\nExecution time:{2}\n{3}";
    private const string DATE_FORMAT = "dd.MM.yyy HH:mm:ss";
    private const string DATE_FILE_FORMAT = "dd-MM-yyyy HH-mm-ss";
    private const string FILE_NAME = "logs {0}.txt";

    private readonly string SEPARATOR = new('=', 50);
    private readonly StringBuilder _logs = new();

    private string? _fileName;
    private bool _useStartDate;
    private DateTime _startTime;

    public DateTime LogTime
        => _useStartDate
        ? _startTime
        : DateTime.Now;

    public string FileName
        => _fileName is null
        ? FILE_NAME.Format(LogTime.ToString(DATE_FILE_FORMAT))
        : _fileName;

    public Logger(string? fileName = null, bool useStartTime = false)
    {
        _fileName = fileName;
        _useStartDate = useStartTime;
        if (_useStartDate)
        {
            _startTime = DateTime.Now;
        }
    }

    public string Log(string methodName, string message)
    {
        var result = LOG_FORMAT.Format(methodName,
                                                  message,
                                                  DateTime.Now.ToString(DATE_FORMAT),
                                                  SEPARATOR);
        _logs.AppendLine(result);
        return result + Environment.NewLine;
    }

    public void Save()
    {
        if (_logs.Length == 0) return;
        File.WriteAllText(Path.Combine(Environment.CurrentDirectory,
                                       FILE_NAME.Format(DateTime.Now.ToString(DATE_FILE_FORMAT))),
                          _logs.ToString());
    }
}
