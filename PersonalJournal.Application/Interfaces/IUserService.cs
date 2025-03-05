using PersonalJournal.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJournal.Application.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<bool> ChangeUserRoleAsync(int userId, string newRole);
        Task<bool> DeleteUserAsync(int userId);
        Task<UserDto?> GetUserAsync(int userId);
    }
}
