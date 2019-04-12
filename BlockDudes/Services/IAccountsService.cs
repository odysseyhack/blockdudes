using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public interface IAccountsService
    {
        decimal GetAccountEtherBalanceAsync(string address);
    }
}
