using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class InsuranceCompanyResponseFormatter
    {
        public static InsuranceCompanyDTO InsuranceCompanyDbToDTO(InsuranceCompany ModelData)
        {
            InsuranceCompanyDTO Record = new InsuranceCompanyDTO
            {
                Id = ModelData.Id,
                CompanyName = ModelData.CompanyName,
            };
            return Record;
        }

        public static IEnumerable<InsuranceCompanyDTO> InsuranceCompanyDbListToDTOList(IEnumerable<InsuranceCompany> ModelData)
        {
            List<InsuranceCompanyDTO> ReturnRecord = new List<InsuranceCompanyDTO>();
            foreach (InsuranceCompany Row in ModelData)
            {
                InsuranceCompanyDTO Record = new InsuranceCompanyDTO
                {

                    Id = Row.Id,
                    CompanyName = Row.CompanyName,

                };
                ReturnRecord.Add(Record);
            }
            return ReturnRecord;


        }
    }
}
