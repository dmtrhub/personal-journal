using PersonalJournal.Application.DTOs;
using PersonalJournal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJournal.Application.Interfaces
{
    public interface IJournalService
    {
        Task<JournalEntryResponseDto?> GetJournalByUserAsync(int id);
        Task<IEnumerable<JournalEntryResponseDto>> GetJournalsByUserAsync();
        Task<JournalEntryResponseDto> AddJournalAsync(JournalEntryRequestDto journalDto);
        Task<bool> UpdateJournalAsync(int id, JournalEntryRequestDto journalDto);
        Task<bool> DeleteJournalAsync(int id);
        Task<IEnumerable<JournalEntryResponseDto>> SearchUserJournalsAsync(string title);
        Task<IEnumerable<JournalEntryResponseDto>> SearchJournalsByAdminAsync(string title);
        Task<JournalEntryResponseDto?> GetJournalByAdminAsync(int id);
        Task<IEnumerable<JournalEntryResponseDto>> GetAllJournalsAsync();
        Task<bool> DeleteJournalByAdminAsync(int id);
    }
}
