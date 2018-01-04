using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IEthnicityService
    {
        IEnumerable<EthnicityDTO> GetEthnicityList();
        EthnicityDTO InsertEthnicity(EthnicityDTO data);
        EthnicityDTO GetEthnicityById(int id);
        int UpdateEthnicity(EthnicityDTO data);
        void DeleteEthnicityById(int id);
    }
}
