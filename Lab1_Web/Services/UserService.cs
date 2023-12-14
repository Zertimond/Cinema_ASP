using Common.Models;
using Dapper;
using Lab1_Data;
using Lab1_Data.Entities;
using Lab1_Web.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab1_Web.Services;

internal sealed class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UserService(AppDbContext appDbContext, ISqlConnectionFactory sqlConnectionFactory)
    {
        _context = appDbContext;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Guid> CreateUser(UserCreateModel model)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Email = model.Email,
            GenderId = model.GenderId
        };
        await using var connection = _sqlConnectionFactory.CreateConnection();
        await connection.OpenAsync();

        var command = new CommandDefinition(
            "INSERT INTO Users (Id, Name, Email, GenderId) " +
            "VALUES (@Id, @Name, @Email, @GenderId)",
            user);
        await connection.ExecuteAsync(command);

        return user.Id;
    }

    public async Task DeleteUser(UserDeleteModel model)
    {
        var user = await _context.Users
                       .FirstOrDefaultAsync(user => user.Id == model.Id)
                   ?? throw new Exception("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsers(int page, int pageSize, string? sortBy, bool? isAsc)
    {
        IQueryable<User> userQuery = sortBy switch
        {
            "Name" when isAsc!.Value => _context.Users.OrderBy(user => user.Name),
            "Name" when !isAsc.Value => _context.Users.OrderByDescending(user => user.Name),
            "Email" when isAsc!.Value => _context.Users.OrderBy(user => user.Email),
            "Email" when !isAsc.Value => _context.Users.OrderByDescending(user => user.Email),
            "Gender" when isAsc!.Value => _context.Users.OrderBy(user => user.GenderId),
            "Gender" when !isAsc.Value => _context.Users.OrderBy(user => user.GenderId),
            _  => _context.Users.OrderBy(user => user.Id),
        };

        var users  = await userQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Include(x => x.Gender)
            .ToListAsync();


        return users;
    }

    public async Task UpdateUser(UserUpdateModel model)
    {
        var user = await _context.Users
                       .FirstOrDefaultAsync(user => user.Id == model.Id)
                   ?? throw new Exception("User not found");

        user.Name = model.Name ?? user.Name;
        user.Email = model.Email ?? user.Email;
        user.GenderId = model.GenderId ?? user.GenderId;

        _context.Users.Update(user);
    }
}