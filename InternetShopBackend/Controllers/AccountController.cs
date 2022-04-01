using InternetShopBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopBackend.Controllers;

[Route("Accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    public IPasswordHasher<AccountRequestModel> _hasher;
    public UnitOfWork _uow;

    public AccountController(IAccountRepository _accountRepository, IPasswordHasher<AccountRequestModel> hasher, UnitOfWork UOW)
    { 
        //accRepository = _accountRepository;
        _hasher = hasher;
        _uow = UOW;
    }

    [HttpGet("GetAccounts")]
    public IEnumerable<Account> GetAccount() => _uow.GetAccounts();
    
    
    [HttpPost("AddAccount")]
    public ActionResult<Account> AddAccount(AccountRequestModel accountRequestModel)
    {
        HashModel hashModel = new HashModel(_hasher);
        Account _accountDomainModel = hashModel.GetAccountDomainModel(accountRequestModel);
        
        try
        {
            //accRepository.Add(_accountDomainModel);
            _uow.AccountRepository.Add(_accountDomainModel);
            _uow.SaveChangeASync();
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
        Account account = _uow.GetAccounts().Where(i => i.Login == _accountRequestModel.Login).FirstOrDefault();

        PasswordVerificationResult result = _hasher.VerifyHashedPassword(_accountRequestModel, account.HashePassword,
            _accountRequestModel.Password);

        if (result == PasswordVerificationResult.Failed)
            return Unauthorized();

        return account;
    }

}