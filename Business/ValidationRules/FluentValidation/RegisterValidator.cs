using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RegisterValidator : AbstractValidator<User>
    {
        public RegisterValidator()
        {
            RuleFor(t => t.IdentityNumber).NotNull().NotEmpty().Length(11).WithMessage("TC Kimlik No alanı doğru girilmedi");
            //RuleFor(t => t.FirstName).NotNull().NotEmpty();
            //RuleFor(t => t.LastName).NotNull().NotEmpty();
            RuleFor(t => t.Email).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}


