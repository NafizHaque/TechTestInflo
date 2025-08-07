using System;

namespace UserManagement.Data.Entities;
public class Log
{
    public int Id { get; set; }

    public string Level { get; set; } = "Information";

    public string Message { get; set; } = string.Empty;

    public DateTime TimeStamp { get; set; }

    public string Xaml { get; set; } = string.Empty;
}
