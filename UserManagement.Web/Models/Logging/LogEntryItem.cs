using Microsoft.Extensions.Logging;
using System;
using UserManagement.Data.Enum;

namespace UserManagement.Web.Models.Logging;

public class LogEntryItem
{
    public int Id { get; set; }

    public LogLevel Level { get; set; }

    public LoggingEvents Operation { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime TimeStamp { get; set; }

    public string Xaml { get; set; } = string.Empty;
}

