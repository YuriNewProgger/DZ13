using InternetShopBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopBackend.Controllers;

[Route("Accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private IAccountRepository accRepository;

    public AccountController(IAccountRepository _accountRepository)
    { 
        accRepository = _accountRepository;
    }

    [HttpGet("GetAccounts")]
    public IEnumerable<Account> GetAccount() => accRepository.Get();
    
    [HttpPost("AddAccount")]
    public ActionResult<Account> AddAccount(Account _account)
    {
        _account.Id = 0;
        int result = accRepository.Add(_account);

        if (result == 0)
        {
            return new ObjectResult(result)
            {
                DeclaredType = typeof(Account),
                StatusCode = StatusCodes.Status400BadRequest
            };
        }
        return new ObjectResult(result)
        {
            DeclaredType = typeof(Account),
            StatusCode = StatusCodes.Status201Created
        };
    }
}