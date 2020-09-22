using PaintingAndSound.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaintingAndSound.DataAccess.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUserAsync();

        Task<User> GetUserAsync(int id);

        Task<User> GetUserAsync(string username, string password);

        Task<User> AddUserAsync(string username, string password);
    }
}
