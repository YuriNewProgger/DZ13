﻿using Microsoft.AspNetCore.Identity;

namespace InternetShopBackend.Models;

public class AccountRequestModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public  string Name { get; set; }
    public string Email { get; set; }
    
}