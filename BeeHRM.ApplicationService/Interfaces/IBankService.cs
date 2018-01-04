using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IBankService
    {
        IEnumerable<BankDTO> GetBankList();
        BankDTO InsertBank(BankDTO data);
        BankDTO GetBankId(int id);
        int UpdateBank(BankDTO data);
        void DeleteBankById(int id);
    }
}
