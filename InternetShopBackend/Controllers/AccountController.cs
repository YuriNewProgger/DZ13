﻿using InternetShopBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopBackend.Controllers;

[Route("Accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAccountRepository accRepository;
    public IPasswordHasher<AccountRequestModel> _hasher;

    public AccountController(IAccountRepository _accountRepository, IPasswordHasher<AccountRequestModel> hasher)
    { 
        accRepository = _accountRepository;
        _hasher = hasher;
    }

    [HttpGet("GetAccounts")]
    public IEnumerable<Account> GetAccount() => accRepository.Get();
    
    
    [HttpPost("AddAccount")]
    public ActionResult<Account> AddAccount(AccountRequestModel accountRequestModel)
    {
        HashModel hashModel = new HashModel(_hasher);
        Account _accountDomainModel = hashModel.GetAccountDomainModel(accountRequestModel);
        
        try
        {
            accRepository.Add(_accountDomainModel);
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }
        return Ok();
    }

    [HttpPost("Authorization")]
    public ActionResult<Account> Authorization(AccountRequestModel _accountRequestModel)
    {
        Console.WriteLine("In Authorization");
        Account account = accRepository.Get().Where(i => i.Login == _accountRequestModel.Login).FirstOrDefault();

        PasswordVerificationResult result = _hasher.VerifyHashedPassword(_accountRequestModel, account.HashePassword,
            _accountRequestModel.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized();

        return account;
    }

}