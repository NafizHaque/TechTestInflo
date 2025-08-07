using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive) =>
        _dataAccess.GetAll<User>().Where(u => u.IsActive == isActive);


    public IEnumerable<User> GetAll() =>
        _dataAccess.GetAll<User>();

    public User GetUserById(long userId) =>
        _dataAccess.GetAll<User>().Where(u=> u.Id == userId).FirstOrDefault() ?? new User();

    public void AddUser(User user) =>
        _dataAccess.Create<User>(user);

    public void EditUser(User user) =>
        _dataAccess.Update<User>(user);

    public void DeleteUser(User user) =>
        _dataAccess.Delete<User>(user);
}
