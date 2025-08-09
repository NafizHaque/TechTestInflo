namespace UserManagement.Web.Models.Logging;

public class LogEntriesListViewModel
{
    public IEnumerable<LogEntryItem> LogEntries { get; set; } = new List<LogEntryItem>();   
}
