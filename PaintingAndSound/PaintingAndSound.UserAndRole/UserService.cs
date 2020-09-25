using Microsoft.EntityFrameworkCore;
using PaintingAndSound.Entities;
using PaintingAndSound.ORM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaintingAndSound.UserAndRole
{
    public class UserService: IUserService
    {
        private readonly HSDbContext _db;

        public UserService(HSDbContext db)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<User> AddUserAsync(string username, string password)
        {
            User user = new User();
            user.Name = username;
            user.PassWord = password;
            await _db.Users.AddAsync(user);
            _db.SaveChanges();
            return user;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(p => p.Name == username && p.PassWord == password);
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(p => p.Id ==id);
        }
    }
}
