using Microsoft.AspNetCore.Identity;

namespace InternetShopBackend.Models;

public class HashModel
{
    public IPasswordHasher<AccountRequestModel> _Hasher;

    public HashModel(IPasswordHasher<AccountRequestModel> _hasher) => _Hasher = _hasher;

    public Account GetAccountDomainModel(AccountRequestModel accountRequestModel) => new Account()
    {
        Id = 0,
        Login = accountRequestModel.Login,
        HashePassword = _Hasher.HashPassword(accountRequestModel, accountRequestModel.Password),
        Name = accountRequestModel.Name,
        Email = accountRequestModel.Email
    };
}