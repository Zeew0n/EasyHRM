using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.ViewModel;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IShiftService
    {
        IEnumerable<ShiftDTO> GetShiftsLIst();
        ShiftDTO InsertShift(ShiftDTO data);
        ShiftDTO GetShiftById(int id);
        int UpdateShift(ShiftDTO res);
        
    }
}
