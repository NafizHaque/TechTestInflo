using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using UserManagement.Data.Entities;
using UserManagement.Data.Enum;

namespace UserManagement.Services.Interfaces;
public interface ILoggerService
{
    void LogInformation(LogLevel level, LoggingEvents operation, string message);

    void LogError(LogLevel level, LoggingEvents operation, Exception? ex, string message);

    IEnumerable<LogEntry> GetLogList();

    LogEntry GetLogById(long logId);

    void DeleteLog(LogEntry log);
}
