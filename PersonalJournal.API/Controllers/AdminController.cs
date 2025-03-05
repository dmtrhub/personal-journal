using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalJournal.Application.DTOs;
using PersonalJournal.Application.Interfaces;
using PersonalJournal.Application.Services;
using PersonalJournal.Domain.Entities;

namespace PersonalJournal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJournalService _journalService;

        public AdminController(IUserService userService, IJournalService journalService)
        {
            _userService = userService;
            _journalService = journalService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserAsync(id);
            return user is not null ? Ok(user) : NotFound($"User with ID {id} not found.");
        }

        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> ChangeUserRole(int id, string newRole)
        {
            var updated = await _userService.ChangeUserRoleAsync(id, newRole);
            return updated ? NoContent() : NotFound($"User with ID {id} not found.");
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            return deleted ? NoContent() : NotFound($"User with ID {id} not found.");
        }

        [HttpGet("journals")]
        public async Task<ActionResult<List<JournalEntry>>> GetAllJournals()
        {
            var journals = await _journalService.GetAllJournalsAsync();
            return Ok(journals);
        }

        [HttpGet("journals/{id}")]
        public async Task<ActionResult<List<JournalEntry>>> GetJournalById(int id)
        {
            var journals = await _journalService.GetAllJournalsAsync();
            return Ok(journals);
        }

        [HttpDelete("journals/{id}")]
        public async Task<IActionResult> DeleteJournal(int id)
        {
            var deleted = await _journalService.DeleteJournalByAdminAsync(id);
            return deleted ? NoContent() : NotFound($"Journal with ID {id} not found.");
        }
    }
}
