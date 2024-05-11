
using Domain.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Behaviours;
public static class ValidatorExtensions
{
    public static IRuleBuilder<T, string> IsPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty().WithMessage(MessageConstants.ExceptionPasswordEmpty)
            .MinimumLength(8).WithMessage(MessageConstants.ExceptionPasswordLength)
            .Matches("[A-Z]").WithMessage(MessageConstants.ExceptionPasswordUppercaseLetter)
            .Matches("[a-z]").WithMessage(MessageConstants.ExceptionPasswordLowercaseLetter)
            .Matches("[0-9]").WithMessage(MessageConstants.ExceptionPasswordDigit)
            .Matches("[^a-zA-Z0-9]").WithMessage(MessageConstants.ExceptionPasswordSpecialCharacter);
        return options;
    }
    public static IRuleBuilder<T, string> IsPhone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .Matches("^0[0-9]{10}$").WithMessage(MessageConstants.ExceptionInvalidPhoneNumber);
    }

}