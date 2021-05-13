using System.Linq;
using FluentValidation;
using JdShops.Entities;

namespace JdShops.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(ShopsDBContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                ;

            RuleFor(x => x.PasswordHash).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.PasswordHash);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);

                    if (emailInUse)
                    {
                        context.AddFailure("Email", "This email address have been used before");
                    }
                        

                });

        }
    }
}