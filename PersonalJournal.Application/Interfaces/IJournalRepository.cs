using PersonalJournal.Domain.Entities;

namespace PersonalJournal.Application.Interfaces
{
    public interface IJournalRepository
    {
        Task<JournalEntry?> GetJournalEntryByIdAsync(int id);
        Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
        Task<IEnumerable<JournalEntry>> GetJournalEntriesByUserAsync(int id);
        Task<JournalEntry?> GetJournalEntryByUserAsync(int userId, int id);
        Task AddJournalEntryAsync(JournalEntry journalEntry);
        Task UpdateJournalEntryAsync(JournalEntry journalEntry);
        Task DeleteJournalEntryAsync(JournalEntry journalEntry);
        Task<IEnumerable<JournalEntry>> SearchJournalsByTitleAsync(string title);
        Task<IEnumerable<JournalEntry>> SearchUserJournalsByTitleAsync(int userId ,string title);
    }
}
