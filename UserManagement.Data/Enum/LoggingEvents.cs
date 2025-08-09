namespace UserManagement.Data.Enum;
public enum LoggingEvents
{
    /// <summary>
    /// The event id to be used when logging calls related to getting users.
    /// </summary>
    GetUsers = 1000,

    /// <summary>
    /// The event id to be used when logging calls related to creating a user.
    /// </summary>
    CreateUser = 1001,

    /// <summary>
    /// The event id to be used when logging calls related to updating a user.
    /// </summary>
    UpdateUser = 1002,

    /// <summary>
    /// The event id to be used when logging calls related to deleting user.
    /// </summary>
    DeleteUser = 1003,

    /// <summary>
    /// The event id to be used when logging calls related to getting a user by id.
    /// </summary>
    GetUserById = 1004,
}
