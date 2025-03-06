using FluentValidation;
using PersonalJournal.Application.DTOs;

namespace PersonalJournal.Application.Validators
{
    public class JournalEntryRequestValidator : AbstractValidator<JournalEntryRequestDto>
    {
        public JournalEntryRequestValidator()
        {
            RuleFor(j => j.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(20).WithMessage("Title must be at most 20 characters.");

            RuleFor(j => j.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("Content must be at least 10 characters.");
        }
    }
}
