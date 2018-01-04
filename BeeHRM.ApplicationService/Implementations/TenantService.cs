using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Net.Mime.MediaTypeNames;

namespace BeeHRM.ApplicationService.Implementations
{
  public  class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TenantService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();

        }

        public TenantDTO GetTenant(string domain)
        {
            //Application application = _unitOfWork.ApplicationRepository.Get(a => a.Domain.Trim() == domain.Trim()).FirstOrDefault();
            //if (application == null)
            //    throw new RecordNotFoundException("Tenant Not Found");
            //return new TenantDTO
            //{
            //    TenantID = application.ApplicationID,
            //    CountryID = application.CountryID.Value,
            //    Country = application.Country.Name,
            //    CultureID = application.CultureID.Value,
            //    Culture = application.Culture.Name,
            //    CultureCode = application.Culture.Code,
            //    Name = application.Name,
            //    Domain = application.Domain,
            //    Logo = application.Logo
            //};
            return null;


        }

        public int GetApplicationId(string userName)
        {
            //    if (string.IsNullOrWhiteSpace(userName))
            //        throw new ArgumentNullException("UserID Cannot Be Null Or Contain Whitespace ");

            //    Application application = _unitOfWork.UsersRepository.Filter(u => u.UserName == userName).FirstOrDefault().Application;

            //    if (application == null)
            //        throw new RecordNotFoundException("Application Not Found For UserId");


            //  return application.ApplicationID;

            return 0;
        }
    }
}
