﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.Users;

public class UserContext : DbContext, IContext<User>
{
    private DbSet<User> Users { get; set; }
    public IQueryable<User> Entities { get; set; }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
        Entities = Users.AsQueryable();
    }

    public void BuildTable()
    {
        Database.EnsureCreatedAsync();
    }

    public async Task<User> Add(User entity)
    {
        var user = (await Users.AddAsync(entity)).Entity;
        await SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> AddRange(IEnumerable<User> entities)
    {
        await Users.AddRangeAsync(entities);
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
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserDbConfiguration());
    }
}