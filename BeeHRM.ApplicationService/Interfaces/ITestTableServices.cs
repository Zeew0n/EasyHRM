using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;
namespace BeeHRM.ApplicationService.Interfaces
{
    public interface ITestTableServices
    {

        List<TestTableDTO> GetTestTableList();
         void CreateTestTable(TestTableDTO Record);
    }
}
