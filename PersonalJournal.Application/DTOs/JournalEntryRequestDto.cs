using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJournal.Application.DTOs
{
    public record JournalEntryRequestDto(string Title, string Content);
}
