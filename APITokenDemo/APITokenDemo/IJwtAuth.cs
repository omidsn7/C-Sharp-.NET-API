using APITokenDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITokenDemo
{
    public interface IJwtAuth
    {
        string Authentication(UserModel user);
    }
}
