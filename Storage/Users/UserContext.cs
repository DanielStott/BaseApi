using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.Users;

public class UserContext : DbContext, IContext<User>
{
    public IQueryable<User> Entities { get; set; }
    private DbSet<User> Users { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
        Entities = Users.AsQueryable();
    }

    public async Task BuildTable()
    {
        await Database.EnsureCreatedAsync();
        await SaveChangesAsync();
    }

    public async Task<User> Add(User entity)
    {
        var user = (await Users.AddAsync(entity)).Entity;
        await SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> AddRange(IEnumerable<User> entities)
    {
        await AddRangeAsync(entities);
        await SaveChangesAsync();
        return entities;
    }

    public async Task<User> Update(User entity)
    {
        var user = Users.Update(entity).Entity;
        await SaveChangesAsync();
        return user;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}