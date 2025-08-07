using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List()
    {
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

        _userService.AddUser(userToBeCreated);

        return Ok(new { message = "User created successfully" });
    }

    [HttpPost]
    public ActionResult EditUser(UserViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest( new { message = "User could not be found!" });
        }

        var userToBeUpdated = _userService.GetUserById(user.Id);

        if (user.Id <= 0 || userToBeUpdated.Id == 0)
        {
            return BadRequest(new { message = "User could not be found!" });
        }

        userToBeUpdated.Forename = user.Forename!;
        userToBeUpdated.Surname = user.Surname!;
        userToBeUpdated.Email = user.Email!;
        userToBeUpdated.IsActive = user.IsActive;
        userToBeUpdated.DateOfBirth = user.DateOfBirth;

        _userService.EditUser(userToBeUpdated);

        return Ok(new { message = "User updated successfully" });
    }


    [HttpDelete]
    public ActionResult DeleteUser(long userId)
    {
        var userToBeDeleted = _userService.GetUserById(userId);

        if (userToBeDeleted.Id == 0)
        {
            return BadRequest(new { message = "User could not be found!" });
        }

        _userService.DeleteUser(userToBeDeleted);

        return Ok(new { message = "User deleted successfully" });
    }
}
