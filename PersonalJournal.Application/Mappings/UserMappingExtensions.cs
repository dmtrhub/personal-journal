using Microsoft.AspNetCore.Identity;
using PersonalJournal.Application.DTOs;
using PersonalJournal.Domain.Entities;

namespace PersonalJournal.Application.Mappings
{
    public static class UserMappingExtensions
    {
        public static User ToDomain(this RegisterDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, dto.Password);
            return user;
        }


        public static UserDto ToDto(this User user) =>
            new(user.Id, user.FirstName, user.LastName, user.Username, user.Email);
    }
}
