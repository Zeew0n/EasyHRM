using BeeHRM.ApplicationService.DTOs;
using BeeHRM.ApplicationService.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IHolidayServices
    {
        IEnumerable<HolidayDTOs> HolidayList();
        int CreateHolidays(HolidayDTOs htd);

        void InsertholidayOffices(int holidayId, int OfficeId);

      HolidayDTOs HolidayListbyId(int id);

        IEnumerable<HolidayOfficesViewModel> HolidayOfficeById(int id);

        void DeleteHoliday(int id);

        void CreateHoliayUpdateAttendnace(HolidayDTOs adt);

         int UpdateHoliday(HolidayDTOs htd);

        void DeleteHolidayOffice(int id);
        void UpdateHoliday(string date);

        HolidayDetailsViewModel HolidayDetails(int id);

    }

}
