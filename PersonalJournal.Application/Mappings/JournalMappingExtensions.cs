using PersonalJournal.Application.DTOs;
using PersonalJournal.Domain.Entities;

namespace PersonalJournal.Application.Mappings
{
    public static class JournalMappingExtensions
    {
        public static JournalEntryResponseDto ToDto(this JournalEntry journalEntry) => 
            new(journalEntry.Id, journalEntry.Title, journalEntry.Content, journalEntry.CreatedAt, journalEntry.UpdatedAt);

        public static JournalEntry AddDomain(this JournalEntryRequestDto request, int userId) => new()
        {
            Title = request.Title,
            Content = request.Content,
            UserId = userId
        };

        public static JournalEntry UpdateDomain(this JournalEntryRequestDto dto, JournalEntry journal)
        {
            journal.Title = dto.Title;
            journal.Content = dto.Content;
            journal.UpdatedAt = DateTime.UtcNow;
            return journal;
        }
    }
}
