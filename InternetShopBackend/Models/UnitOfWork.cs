namespace InternetShopBackend.Models;

public class UnitOfWork
{
    public IAccountRepository AccountRepository { get; }
    public AppDbContext Context { get; }

    public UnitOfWork(IAccountRepository accountRepository, AppDbContext context)
    {
        AccountRepository = accountRepository;
        Context = context;
    }

    public List<Account> GetAccounts() => AccountRepository.Get();

    public Task SaveChangeASync() => Context.SaveChangesAsync();
}