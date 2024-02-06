using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext
{
    public interface IJwtConnection
    {
        Task<JwtUsers?> Login(JwtUsers user);
    }
}
