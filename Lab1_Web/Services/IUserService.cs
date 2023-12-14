using Common.Models;
using Lab1_Data.Entities;

namespace Lab1_Web.Services;

public interface IUserService
{
    Task<Guid> CreateUser(UserCreateModel model);
    Task DeleteUser(UserDeleteModel model);
    Task UpdateUser(UserUpdateModel model);
    Task<List<User>> GetAllUsers(int page, int pageSize, string? sortBy, bool? isAsc);
}
