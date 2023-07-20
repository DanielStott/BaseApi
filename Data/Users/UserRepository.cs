using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Users;

public class UserRepository : IUserRepository
{
    private readonly IContext<User> _context;

    public UserRepository(IContext<User> context) => _context = context;

    public async Task<User?> GetByEmailOrUsername(string email, string username) =>
        await _context.Entities
            .FirstOrDefaultAsync(e => e.Email == email || e.Username == username);

    public async Task<User?> GetById(Guid id) =>
        await _context.Entities
            .FirstOrDefaultAsync(e => e.Id == id);

    public async Task<User?> Get(User entity) =>
        await _context.Entities
            .FirstOrDefaultAsync(e => e == entity);

    public async Task<IEnumerable<User?>> GetAll() =>
        await _context.Entities
            .ToListAsync();

    public async Task<User> Add(User entity) =>
        await _context.Add(entity);

    public async Task<IEnumerable<User>> AddRange(IEnumerable<User> entities) =>
        await _context.AddRange(entities);

    public async Task<User> Update(User entity) =>
        await _context.Update(entity);
}