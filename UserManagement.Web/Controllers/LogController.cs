using System.Linq;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Logging;

namespace UserManagement.Web.Controllers;
public class LogController : Controller
{
    private readonly ILoggerService _logger;
    public LogController(ILoggerService logger) => _logger = logger;

    [HttpGet]
    public ViewResult LogList()
    {
        var items = _logger.GetLogList().Select(p => new LogEntryItem
        {
            Id = p.Id,
            Level = p.Level,
            Operation = p.Operation,
            Message = p.Message,
            TimeStamp = p.TimeStamp,
            Xaml = p.Xaml,
        });

        var model = new LogEntriesListViewModel
        {
            LogEntries = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    public PartialViewResult ViewLogPartial(long logId)
    {
        var log = _logger.GetLogById(logId);

        var model = new LogEntryItem
        {
            Id = log.Id,
            Level = log.Level,
            Operation = log.Operation,
            Message = log.Message,
            TimeStamp = log.TimeStamp,
            Xaml = log.Xaml,

            
        };

        return PartialView("_ViewLogPartial", model);
    }

    [HttpDelete]
    public ActionResult DeleteLog(long logId)
    {
        var logToBeDeleted = _logger.GetLogById(logId);

        if (logToBeDeleted.Id == 0)
        {
            return BadRequest(new { message = "Log could not be found!" });
        }

        _logger.DeleteLog(logToBeDeleted);

        return Ok(new { message = "Log deleted successfully" });
    }
}
