﻿using FluentValidation;
using Meetings4IT.Shared.Implementations.Search;

namespace Meetings4IT.Shared.Implementations.Validators;

public class SortValidator : AbstractValidator<SortModel>
{
    public SortValidator(string[] availableNames)
    {
        When(r => !string.IsNullOrEmpty(r.Name), () =>
        {
            RuleFor(r => r.Name).Custom((sort, context) =>
            {
                if (!string.IsNullOrWhiteSpace(sort) &&
                    !availableNames.Contains(sort, StringComparer.OrdinalIgnoreCase))
                    context.AddFailure("SortName",
                        $"Sort name must in [{string.Join(", ", availableNames)}]");
            });
        });
    }
}