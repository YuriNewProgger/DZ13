﻿using System.Net.Http.Headers;
using System.Security.Claims;
using InternetShopBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace InternetShopBackend.Controllers;

[Route("Accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private IPasswordHasher<AccountRequestModel> _hasher;
    private UnitOfWork _uow;
    private readonly ServiceToken _serviceToken;

    public AccountController(IAccountRepository _accountRepository, IPasswordHasher<AccountRequestModel> hasher,
        UnitOfWork UOW, ServiceToken serviceToken)
    {
        _hasher = hasher;
        _uow = UOW;
        _serviceToken = serviceToken;
    }

    //[Authorize]
    [HttpGet("GetAccounts")]
    public IEnumerable<Account> GetAccount()
    {
        var striId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var acc = _uow.AccountRepository.GetAccountById(int.Parse(striId));
        Log.Information(acc.Name);
        return _uow.AccountRepository.Get();
    }


    [HttpPost("AddAccount")]
    public ActionResult<Account> AddAccount(AccountRequestModel accountRequestModel)
    {
        HashModel hashModel = new HashModel(_hasher);
        Account _accountDomainModel = hashModel.GetAccountDomainModel(accountRequestModel);
        Basket basket = new Basket();
        basket.IdAcc = _accountDomainModel.Id;
        try
        {
            _uow.AccountRepository.Add(_accountDomainModel);
            _uow.BasketRepository.Add(basket);
            _uow.SaveChangeASync();
        }
        catch (Exception ex)
        {
            return Unauthorized();
        }

        return Ok();
    }

    [HttpPost("Login")]
    public ActionResult<string> Login(AccountRequestModel _accountRequestModel)
    {
        Console.WriteLine("In Authorization");
        Account account = _uow.AccountRepository.Get().Where(i => i.Login == _accountRequestModel.Login)
            .FirstOrDefault();

        PasswordVerificationResult result = _hasher.VerifyHashedPassword(_accountRequestModel, account.HashePassword,
            _accountRequestModel.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized();

        var token = _serviceToken.GenerateToken(account);
        return token;
    }
}