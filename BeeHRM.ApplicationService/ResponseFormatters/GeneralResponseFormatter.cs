using AutoMapper;
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ResponseFormatters
{
    public class GeneralResponseFormatter
    {
        //public static IEnumerable<EmployeeDetailsViewModel> SearchEmpToDetailVM(IEnumerable<SP_SearchEmployees_Result> modelData)
        //{
        //    //var config = new MapperConfiguration(cfg => {
        //    //    cfg.CreateMap<IEnumerable<EmployeeDetailsViewModel>, IEnumerable<SP_SearchEmployees_Result>>();
        //    //});

        //    //IMapper mapper = config.CreateMapper();
        //    //var source = new EmployeeDetailsViewModel();
        //    //return mapper.Map<EmployeeDetailsViewModel, SP_SearchEmployees_Result>(modelData);
        //    Mapper.CreateMap<EmployeeDetailsViewModel, SP_SearchEmployees_Result>().ConvertUsing(

        //        m =>
        //        {
        //            return new EmployeeDetailsViewModel
        //            {
        //                Name = m.EmpName,
        //                Department = m.

        //            };

        //        }
        //        );
        //    return Mapper.Map<IEnumerable<EmployeeDetailsViewModel>, IEnumerable<SP_SearchEmployees_Result>>(modelData);
        //}
    }
}
