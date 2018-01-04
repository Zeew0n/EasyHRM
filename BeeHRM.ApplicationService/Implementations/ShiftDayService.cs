using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class ShiftDayService : IShiftDayService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ShiftDay _shiftDay;
        public ShiftDayService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _shiftDay = new ShiftDay();
        }

        public ShiftDayDTO InsertShiftDayDetail(ShiftDayDTO detail)
        {
            ShiftDay dataToInsert = new ShiftDay();
            dataToInsert = ShiftDayRequestFormatter.ConvertRespondentInfoFromDTO(detail);
            return ShiftDayRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.ShiftDayRepository.Create(dataToInsert));
           // return null;
        }

        public List<ShiftDayDTO> GetShiftByParentId(int shiftId)
        {
            var shiftDayList = _unitOfWork.ShiftDayRepository.All();
            List<ShiftDayDTO> res = ShiftDayResponseFormatter.ModelData(from sdl in shiftDayList
                                                                        where sdl.DayShiftId == shiftId
                                                                        select sdl).ToList();
            return res;
            //return null;

        }

        public int UpdateShiftDetail(ShiftDayDTO detail)
        {
            ShiftDay dataToUpdate = new ShiftDay();
            dataToUpdate = ShiftDayRequestFormatter.ConvertRespondentInfoFromDTO(detail);
            var res = _unitOfWork.ShiftDayRepository .Update(dataToUpdate);
            _unitOfWork.Save();
            return res;
           // return 0;
        }
    }
}
