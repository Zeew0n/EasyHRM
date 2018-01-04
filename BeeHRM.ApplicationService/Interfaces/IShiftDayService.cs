using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IShiftDayService
    {
        ShiftDayDTO InsertShiftDayDetail(ShiftDayDTO detail);
        List<ShiftDayDTO> GetShiftByParentId(int shiftId);
        int UpdateShiftDetail(ShiftDayDTO detail);
    }
}
