using Microsoft.AspNetCore.Http;
using PersonalJournal.Application.DTOs;
using PersonalJournal.Application.Interfaces;
using PersonalJournal.Application.Mappings;
using System.Security.Claims;

namespace PersonalJournal.Application.Services
{
    public class JournalService : IJournalService
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JournalService(IJournalRepository journalRepository, IHttpContextAccessor httpContextAccessor)
        {
            _journalRepository = journalRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<JournalEntryResponseDto> AddJournalAsync(JournalEntryRequestDto journalDto)
        {
            var journal = journalDto.AddDomain(GetCurrentUserId());

            await _journalRepository.AddJournalEntryAsync(journal);

            if (journal == null)
            {
                throw new Exception("Journal entry was not created successfully.");
            }
            return journal.ToDto();
        }

        public async Task<bool> DeleteJournalAsync(int id)
        {
            var journal = await _journalRepository.GetJournalEntryByUserAsync(GetCurrentUserId() ,id);
            if (journal is null) return false;

            await _journalRepository.DeleteJournalEntryAsync(journal);
            return true;
        }

        public async Task<IEnumerable<JournalEntryResponseDto>> GetJournalsByUserAsync()
        {
            var journals = await _journalRepository.GetJournalEntriesByUserAsync(GetCurrentUserId());
            return journals.Select(j => j.ToDto()).ToList();
        }

        public async Task<JournalEntryResponseDto?> GetJournalByUserAsync(int id)
        {
            var journal = await _journalRepository.GetJournalEntryByUserAsync(GetCurrentUserId(), id);
            return journal?.ToDto();
        }

        public async Task<IEnumerable<JournalEntryResponseDto>> SearchUserJournalsAsync(string title)
        {
            var journals = await _journalRepository.SearchUserJournalsByTitleAsync(GetCurrentUserId() ,title);
            return journals.Select(j => j.ToDto()).ToList();
        }

        public async Task<bool> UpdateJournalAsync(int id, JournalEntryRequestDto journalEntry)
        {
            var journal = await _journalRepository.GetJournalEntryByUserAsync(GetCurrentUserId() ,id);
            if(journal is null) return false;

            var updated = journalEntry.UpdateDomain(journal);
            await _journalRepository.UpdateJournalEntryAsync(updated);

            return true;
        }

        //---ADMIN METHODS---
        public async Task<bool> DeleteJournalByAdminAsync(int id)
        {
            var journal = await _journalRepository.GetJournalEntryByIdAsync(id);
            if (journal is null) return false;

            await _journalRepository.DeleteJournalEntryAsync(journal);
            return true;
        }

        public async Task<IEnumerable<JournalEntryResponseDto>> GetAllJournalsAsync()
        {
            var journals = await _journalRepository.GetJournalEntriesAsync();
            return journals.Select(j => j.ToDto()).ToList();
        }

        public async Task<JournalEntryResponseDto?> GetJournalByAdminAsync(int id)
        {
            var journal = await _journalRepository.GetJournalEntryByIdAsync(id);
            return journal?.ToDto();
        }

        public async Task<IEnumerable<JournalEntryResponseDto>> SearchJournalsByAdminAsync(string title)
        {
            var journals = await _journalRepository.SearchJournalsByTitleAsync(title);
            return journals.Select(j => j.ToDto()).ToList();
        }
        //---END ADMIN METHODS---

        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User ID is not found in the token.");
            }

            return int.Parse(userId);
        }

    }
}
