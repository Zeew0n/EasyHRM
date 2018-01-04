using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ResponseFormatters;
using System.Web.Mvc;
using BeeHRM.Repository;

namespace BeeHRM.ApplicationService.Implementations
{
   public class CountryService:ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private dbBeeHRMEntities db;
        public CountryService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public IEnumerable<SelectListItem> GetCountryList()
        {
            List<SelectListItem> CountryList = new List<SelectListItem>();
            foreach (var row in _unitOfWork.CountryReposityory.All())
            {
                CountryList.Add(new SelectListItem
                {
                    Text = row.CountryName,
                    Value = row.CountryId.ToString()
                });
            }
            return CountryList;
        }

       public  string GetCountryName(int id)
        {
            using (db= new dbBeeHRMEntities ())
            {
                var data = db.Countries.Find(id);
                return data.CountryName;

            }
        }
    }
}
