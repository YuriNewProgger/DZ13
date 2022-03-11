﻿using Microsoft.EntityFrameworkCore;

namespace InternetShopBackend.Models;

public class AccountRepository : IRepository<Account>
{
    public AppDbContext Context;
    public AccountRepository(AppDbContext _context) => Context = _context;

    public List<Account> Get() => Context.Accounts.ToList();

    public void Add(Account account)
    {
        Context.Accounts.Add(account);
        Context.SaveChanges();
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
}