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
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class RankService : IRankService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RankService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public RankDTO GetRankByID(int rankId)
        {
            RankDTO rank = RankRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.RankRepository.GetById(rankId));
            return rank;
        }

        public IEnumerable<RankDTO> GetRankList()
        {
            IEnumerable<Rank> modelDatas = _unitOfWork.RankRepository.All().ToList();
            return RankResponseFormatter.ModelData(modelDatas);
        }

        public RankDTO InsertRank(RankDTO data)
        {
            Rank rnk = RankRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return RankRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.RankRepository.Create(rnk));
        }

        public int UpdateRank(RankDTO data)
        {
            Rank rnk = RankRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return (_unitOfWork.RankRepository.Update(rnk));
        }
    }
}
