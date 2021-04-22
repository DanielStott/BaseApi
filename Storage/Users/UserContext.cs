using System.Linq;
using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Storage.Users
{
    public class UserContext : DbContext, IContext<User>
    {
        private DbSet<User> Users { get; set; }
        public IQueryable<User> Entities { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }

        public async Task<User> Add(User entity)
        {
            return (await Users.AddAsync(entity)).Entity;
        }
    }
}