using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class TaskValidator : AbstractValidator<Task>
    {
        /*Nesneye girilen değerlerle ilgili validationlar*/
        public TaskValidator()
        {
            RuleFor(t => t.StartDate).NotNull();
            RuleFor(t => t.EndDate).NotNull();

            RuleFor(m => m.StartDate)
            .NotEmpty()
            .WithMessage("Başlangıç Tarihi girilmedi");

            RuleFor(m => m.EndDate)
                .NotEmpty().WithMessage("Bitiş Tarihi girilmedi")
                .GreaterThan(m => m.StartDate.Value)
                                .WithMessage("Bitiş Tarihi Başlangıç Taarihinden büyük veya eşit olmalıdır")
                .When(m => m.StartDate.HasValue);

            RuleFor(t => t.Subject).NotEmpty();
            RuleFor(t => t.Subject).Length(3, 150);

            RuleFor(t => t.Description).NotEmpty();
        }
    }
}
