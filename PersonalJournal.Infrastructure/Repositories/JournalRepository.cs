using Microsoft.EntityFrameworkCore;
using PersonalJournal.Application.DTOs;
using PersonalJournal.Application.Interfaces;
using PersonalJournal.Domain.Entities;
using PersonalJournal.Infrastructure.Data;

namespace PersonalJournal.Infrastructure.Repositories
{
    public class JournalRepository : IJournalRepository
    {
        private readonly AppDbContext _context;

        public JournalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddJournalEntryAsync(JournalEntry journalEntry)
        {
            try
            {
                await _context.JournalEntries.AddAsync(journalEntry);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving journal entry: {ex.Message}");
            }
        }

        public async Task DeleteJournalEntryAsync(JournalEntry journalEntry)
        {
            _context.JournalEntries.Remove(journalEntry);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<JournalEntry>> GetJournalEntriesByUserAsync(int userId) =>
            await _context.JournalEntries.Where(j => j.UserId == userId).ToListAsync();

        public async Task<JournalEntry?> GetJournalEntryByUserAsync(int userId, int id) =>
            await _context.JournalEntries.Where(j => j.UserId == userId && j.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<JournalEntry>> SearchUserJournalsByTitleAsync(int userId ,string title) =>
            await _context.JournalEntries.Where(j => j.Title.Contains(title) && j.UserId == userId).ToListAsync();

        public async Task UpdateJournalEntryAsync(JournalEntry journalEntry)
        {
            _context.JournalEntries.Update(journalEntry);
            await _context.SaveChangesAsync();
        }

        //ADMIN
        public async Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync() =>
            await _context.JournalEntries.ToListAsync();

        public async Task<JournalEntry?> GetJournalEntryByIdAsync(int id) =>
            await _context.JournalEntries.FindAsync(id);

        public async Task<IEnumerable<JournalEntry>> SearchJournalsByTitleAsync(string title) =>
            await _context.JournalEntries.Where(j => j.Title.Contains(title)).ToListAsync();
        //ADMIN
    }
}
