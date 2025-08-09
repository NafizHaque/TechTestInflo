using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using UserManagement.Data;
using UserManagement.Data.Entities;
using UserManagement.Data.Enum;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.Implementations;
public class LoggerService : ILoggerService
{
    private readonly IDataContext _dataAccess;
    private readonly ILogger<LoggerService> _logger;
    public LoggerService(IDataContext dataAccess, ILogger<LoggerService> logger)
    {
        _dataAccess = dataAccess;
        _logger = logger;

    }

    public void LogInformation(LogLevel level, LoggingEvents operation, string message)
    {

        _logger.LogInformation((int)level, message);



        _dataAccess.Create<LogEntry>(new LogEntry
        {

            Level = level,
            Operation = operation,
            TimeStamp = DateTime.UtcNow,
            Message = message,
            Xaml = string.Empty
        });
    }

    public void LogError(LogLevel level, LoggingEvents operation, Exception? ex, string message)
    {

        _logger.LogInformation((int)level, ex, message);

        _dataAccess.Create<LogEntry>(new LogEntry
        {

            Level = level,
            Operation = operation,
            TimeStamp = DateTime.UtcNow,
            Message = message,
            Xaml = ex?.ToString() ?? string.Empty
        });
    }


    public IEnumerable<LogEntry> GetLogList()
    {
        return _dataAccess.GetAll<LogEntry>();
    }

    public LogEntry GetLogById(long logId)
    {
        return _dataAccess.GetAll<LogEntry>().Where(u => u.Id == logId).FirstOrDefault() ?? new LogEntry();
    }

    public void DeleteLog(LogEntry log)
    {
        _dataAccess.Delete<LogEntry>(log);
    }
}
