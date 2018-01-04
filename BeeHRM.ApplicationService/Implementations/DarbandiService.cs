using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class DarbandiService : IDarbandiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Darbandi _darbandi;

        public DarbandiService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
            _darbandi = new Darbandi();
        }

        public IEnumerable<DarbandiDTO> GetDarbandilist()
        {
            var darbandiList = _unitOfWork.DarbandiRepository.All();
            var officeList = _unitOfWork.OfficeRepository.All();
            var desgList = _unitOfWork.DesignationRepository.All();

            IEnumerable<DarbandiDTO> list = (from darb in darbandiList
                                             join ol in officeList on darb.DarbandiOfficeId equals ol.OfficeId
                                             join dl in desgList on darb.DarbandiDesgId equals dl.DsgId
                                             select new DarbandiDTO
                                             {
                                                 DarbandiDate = darb.DarbandiDate,
                                                 DarbandiDesgId = darb.DarbandiDesgId,
                                                 DarbandiId = darb.DarbandiId,
                                                 DarbandiNumber = darb.DarbandiNumber,
                                                 DarbandiOfficeId = darb.DarbandiOfficeId,
                                                 DarbandiRemarks = darb.DarbandiRemarks,
                                                 DarbandiType = darb.DarbandiType,
                                                 DesgName = dl.DsgName,
                                                 OfficeName = ol.OfficeName
                                             }).ToList();
            return list;
        }

        public DarbandiDTO InsertDarabandi(DarbandiDTO data)
        {
            

            Darbandi darbandi = DarbandiRequestFormatter.ConvertRespondentInfoFromDTO(data);
            _darbandi.DarbandiDate = darbandi.DarbandiDate;
            _darbandi.DarbandiDesgId = darbandi.DarbandiDesgId;
            _darbandi.DarbandiId = darbandi.DarbandiId;
            _darbandi.DarbandiNumber = darbandi.DarbandiNumber;
            _darbandi.DarbandiOfficeId = darbandi.DarbandiOfficeId;
            _darbandi.DarbandiRemarks = darbandi.DarbandiRemarks;
            _darbandi.DarbandiType = darbandi.DarbandiType;
            return DarbandiRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.DarbandiRepository.Create(darbandi));
        }

        public DarbandiDTO GetDarbandiById(int darbandiId)
        {
            var darbandiList = _unitOfWork.DarbandiRepository.All();
            var officeList = _unitOfWork.OfficeRepository.All();
            var desgList = _unitOfWork.DesignationRepository.All();

            DarbandiDTO list = (from darb in darbandiList
                                             join ol in officeList on darb.DarbandiOfficeId equals ol.OfficeId
                                             join dl in desgList on darb.DarbandiDesgId equals dl.DsgId
                                             select new DarbandiDTO
                                             {
                                                 DarbandiDate = darb.DarbandiDate,
                                                 DarbandiDesgId = darb.DarbandiDesgId,
                                                 DarbandiId = darb.DarbandiId,
                                                 DarbandiNumber = darb.DarbandiNumber,
                                                 DarbandiOfficeId = darb.DarbandiOfficeId,
                                                 DarbandiRemarks = darb.DarbandiRemarks,
                                                 DarbandiType = darb.DarbandiType,
                                                 DesgName = dl.DsgName,
                                                 OfficeName = ol.OfficeName
                                             }).Where(x=> x.DarbandiId == darbandiId).FirstOrDefault();
            return list;
        }

        public int UpdateDarabandi(DarbandiDTO data)
        {
            
            Darbandi darb = DarbandiRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.DarbandiRepository.Update(darb);
            _unitOfWork.Save();
            return response;            
             
            
            
            
            
              

        }
    }
}
