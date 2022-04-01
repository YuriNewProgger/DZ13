namespace InternetShopBackend.Models;

public class UnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public IBasketRepository BasketRepository { get; }
    public AppDbContext Context { get; }

    public UnitOfWork(IAccountRepository accountRepository, IBasketRepository basketRepository, AppDbContext context)
    {
        AccountRepository = accountRepository;
        BasketRepository = basketRepository;
        Context = context;
    }

    public List<Account> GetAccounts() => AccountRepository.Get();

    public Task SaveChangeASync() => Context.SaveChangesAsync();
}