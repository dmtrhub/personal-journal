using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJournal.Application.DTOs
{
    public record RegisterDto(string FirstName, string LastName, string Username, string Email, string Password);
}
