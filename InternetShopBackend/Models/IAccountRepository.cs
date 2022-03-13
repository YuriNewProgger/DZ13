namespace InternetShopBackend.Models;

public interface IAccountRepository: IRepository<Account>
{
    Account GetAccountById(int id);
}