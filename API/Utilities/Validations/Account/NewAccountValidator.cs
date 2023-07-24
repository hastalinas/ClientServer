﻿using API.Contracts;
using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Account;

public class NewAccountValidator : AbstractValidator<NewAccountDto>
{ 
    private readonly IAccountRepository _accountRepository;
    public NewAccountValidator(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
        RuleFor(ac => ac.Guid).NotEmpty();
        RuleFor(ac => ac.Password).NotEmpty().Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        RuleFor(ac => ac.OTP).NotEmpty();
        RuleFor(ac => ac.IsUsed).NotEmpty();
        RuleFor(ac => ac.ExpiredTime).NotEmpty().GreaterThanOrEqualTo(DateTime.Now);

    }
}
