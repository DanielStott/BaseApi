﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Users.Interfaces;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IContext<User> _context;
        
        public UserRepository(IContext<User> context)
        {
            _context = context;
        }
        
        public async Task<User> GetById(Guid id)
        {
            return await _context.Entities
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> Get(User entity)
        {
            return await _context.Entities
                .FirstOrDefaultAsync(e => e == entity);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Entities
                .ToListAsync();
        }

        public async Task<User> Add(User entity)
        {
            return await _context.Add(entity);
        }
    }
}