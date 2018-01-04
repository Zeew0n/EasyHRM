using BeeHRM.ApplicationService.Interfaces;
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
using BeeHRM.ApplicationService.ViewModel;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class ShiftService : IShiftService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShiftService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public ShiftDTO GetShiftById(int id)
        {
            return ShiftRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.ShiftRepository.GetById(id));
        }

        public IEnumerable<ShiftDTO> GetShiftsLIst()
        {
            IEnumerable<Shift> modelDatas = _unitOfWork.ShiftRepository.All().ToList();
            return ShiftResponseFormatter.ModelData(modelDatas);
        }

        public ShiftDTO InsertShift(ShiftDTO data)
        {
            Shift dataToInsert = new Shift();
            dataToInsert = ShiftRequestFormatter.ConvertRespondentInfoFromDTO(data);
            ShiftDTO res = ShiftRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.ShiftRepository.Create(dataToInsert));
            return res;
        }

       

       public  int UpdateShift(ShiftDTO res)
        {
            Shift dataToUpdate = new Shift();
            dataToUpdate = ShiftRequestFormatter.ConvertRespondentInfoFromDTO(res);
            var result =  _unitOfWork.ShiftRepository.Update(dataToUpdate);
            _unitOfWork.Save();
            return result;

        }
    }
}
