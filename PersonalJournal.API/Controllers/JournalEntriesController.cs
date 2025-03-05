using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalJournal.Application.DTOs;
using PersonalJournal.Application.Interfaces;
using PersonalJournal.Domain.Entities;

namespace PersonalJournal.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class JournalEntriesController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalEntriesController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpGet]
        public async Task<ActionResult<List<JournalEntry>>> GetAll()
        {
            var journals = await _journalService.GetJournalsByUserAsync();
            return journals.Any() ? Ok(journals) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create(JournalEntryRequestDto dto)
        {
            var journal = await _journalService.AddJournalAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = journal.Id }, journal);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntry>> GetById(int id)
        {
            var journal = await _journalService.GetJournalByUserAsync(id);
            return journal is not null ? Ok(journal) : NotFound($"Journal with ID {id} not found.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _journalService.DeleteJournalAsync(id);
            return deleted ? NoContent() : NotFound($"Journal with ID {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, JournalEntryRequestDto dto)
        {
            var updated = await _journalService.UpdateJournalAsync(id, dto);
            return updated ? NoContent() : NotFound($"Journal with ID {id} not found.");
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<JournalEntry>>> SearchByTitle(string title)
        {
            var journals = await _journalService.SearchUserJournalsAsync(title);
            return journals.Any() ? Ok(journals) : NotFound($"Journal with title \"{title}\" not found.");
        }
    }
}
