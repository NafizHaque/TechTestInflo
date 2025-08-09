using System.Linq;
using Microsoft.Extensions.Logging;
using UserManagement.Data.Enum;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Services.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly ILoggerService _logger;
    public UsersController(IUserService userService, ILoggerService logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public ViewResult List()
    {
        _logger.LogInformation(LogLevel.Information, LoggingEvents.GetUsers, "Getting all Users");

        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet]
    public ViewResult ActiveUsersFilter(bool isActive)
    {
        var items = _userService.FilterByActive(isActive).Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        });

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View("List", model);
    }

    [HttpGet]
    public PartialViewResult ViewUserPartial(long userId)
    {
        _logger.LogInformation(LogLevel.Information, LoggingEvents.GetUserById, $"Getting User by id: {userId}");
        var user = _userService.GetUserById(userId);

        var model = new UserListItemViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth
        };

        return PartialView("_ViewUserPartial", model);
    }

    [HttpGet]
    public PartialViewResult UserAddFormPartial()
    {
        return PartialView("_UserFormPartial", new UserViewModel() { NewUserFlag = true});
    }

    [HttpGet]
    public PartialViewResult UserEditFormPartial(long userId)
    {
        _logger.LogInformation(LogLevel.Information, LoggingEvents.GetUserById, $"Getting User by id: {userId}");
        var user = _userService.GetUserById(userId);

        var model = new UserViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
            NewUserFlag = false

        };

        return PartialView("_UserFormPartial", model);
    }

    [HttpPost]
    public ActionResult AddUser(UserViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToBeCreated = new User
        {
            Forename = user.Forename ?? string.Empty,
            Surname = user.Surname ?? string.Empty,
            Email = user.Email ?? string.Empty,
            IsActive = true,
            DateOfBirth = user.DateOfBirth
        };

        _logger.LogInformation(LogLevel.Information, LoggingEvents.CreateUser, $"Creating new user. Name: {userToBeCreated.Forename} {userToBeCreated.Surname}");
        _userService.AddUser(userToBeCreated);

        return Ok(new { message = "User created successfully" });
    }

    [HttpPost]
    public ActionResult EditUser(UserViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userToBeUpdated = _userService.GetUserById(user.Id);

        if (user.Id <= 0 || userToBeUpdated.Id == 0)
        {
            _logger.LogError(LogLevel.Error, LoggingEvents.UpdateUser, null, $"Could not find User: {userToBeUpdated.Id}");
            return BadRequest(new { message = "User could not be found!" });
        }

        userToBeUpdated.Forename = user.Forename!;
        userToBeUpdated.Surname = user.Surname!;
        userToBeUpdated.Email = user.Email!;
        userToBeUpdated.IsActive = user.IsActive;
        userToBeUpdated.DateOfBirth = user.DateOfBirth;

        _logger.LogInformation(LogLevel.Information, LoggingEvents.UpdateUser, $"Editting User id: {userToBeUpdated.Id}");
        _userService.EditUser(userToBeUpdated);

        return Ok(new { message = "User updated successfully" });
    }


    [HttpDelete]
    public ActionResult DeleteUser(long userId)
    {
        var userToBeDeleted = _userService.GetUserById(userId);

        if (userToBeDeleted.Id == 0)
        {
            _logger.LogError(LogLevel.Error, LoggingEvents.DeleteUser, null, $"Could not find User id: {userToBeDeleted.Id}");
            return BadRequest(new { message = "User could not be found!" });
        }

        _logger.LogInformation(LogLevel.Information, LoggingEvents.DeleteUser, $"Deleting User id: {userToBeDeleted.Id}");
        _userService.DeleteUser(userToBeDeleted);

        return Ok(new { message = "User deleted successfully" });
    }
}
