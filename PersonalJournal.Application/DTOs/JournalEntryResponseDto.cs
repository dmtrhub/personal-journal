﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalJournal.Application.DTOs
{
    public record JournalEntryResponseDto(int Id, string Title, string Content, DateTime createdAt, DateTime updatedAt) { }
}
