using BetaCustomers.API.Models;
using FluentValidation;

namespace BetaCustomers.API.Validators;

public class UsersValidator : AbstractValidator<UserModel>
{
    public UsersValidator()
    {
        RuleFor(x => x.Username).NotEmpty().Length(3, 20).WithMessage("Username is required!");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required!");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required!");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required!");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Password is required!");
    }
}