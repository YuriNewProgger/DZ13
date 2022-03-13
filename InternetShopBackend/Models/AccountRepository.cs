using Microsoft.EntityFrameworkCore;

namespace InternetShopBackend.Models;

public class AccountRepository : IAccountRepository
{
    public AppDbContext Context;
    public AccountRepository(AppDbContext _context) => Context = _context;

    public List<Account> Get() => Context.Accounts.ToList();

    public int Add(Account account)
    {
        Context.Accounts.Add(account);
        int result = Context.SaveChanges();
        return result;
    } 

    public void Update(Account account)
    {
        Context.Entry(account).State = EntityState.Modified;
        Context.SaveChanges();
    }
    public void Delete(Account account)
    {
        Context.Entry(account).State = EntityState.Deleted;
        Context.SaveChanges();
    }

    public Account GetAccountById(int id) => Context.Accounts.Where(i => i.Id == id).FirstOrDefault();
    
}