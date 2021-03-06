using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InternetShopBackend.Models;

public class AccountRepository : IAccountRepository
{
    public AppDbContext Context;

    public AccountRepository(AppDbContext _context)
    {
        Context = _context;
    }

    public List<Account> Get() => Context.Accounts.ToList();

    public void Add(Account account)
    {
        Context.Accounts.AddAsync(account);
    } 

    public void Update(Account account)
    {
        Context.Entry(account).State = EntityState.Modified;
    }
    public void Delete(Account account)
    {
        Context.Entry(account).State = EntityState.Deleted;
    }

    public Account GetAccountById(int id) => Context.Accounts.Where(i => i.Id == id).FirstOrDefault();
    
}