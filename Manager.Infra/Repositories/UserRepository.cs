using Manager.Domain.Entities;
using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ManagerDbContext _context;
        public UserRepository(ManagerDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email.ToLower() == email.ToLower())
                .AsNoTracking()
                .ToListAsync();

            return user.FirstOrDefault();
        }

        public async Task<List<User>> GetSearchByEmail(string email)
        {
            var users = await _context.Users
                .Where(x => x.Email.ToLower().Contains(email.ToLower()))
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<List<User>> SearchByName(string name)
        {
            var allUsers = await _context.Users
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .AsNoTracking()
                .ToListAsync();

            return allUsers;
        }
    }
}
