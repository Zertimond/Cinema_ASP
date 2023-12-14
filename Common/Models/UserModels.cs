using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_Data.Entities;

namespace Common.Models;

public record UserCreateModel (string Name, string Email, Guid? GenderId);
public record UserUpdateModel (Guid Id, string? Name, string? Email, Guid? GenderId);
public record UserDeleteModel(Guid Id);
public record GetUsersModel(List<User> Users, int PageSize, int Page);
