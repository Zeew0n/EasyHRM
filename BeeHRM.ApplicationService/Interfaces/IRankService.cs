using BeeHRM.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Interfaces
{
    public interface IRankService
    {
        IEnumerable<RankDTO> GetRankList();
        RankDTO InsertRank(RankDTO data);
        RankDTO GetRankByID(int rankId);
        int UpdateRank(RankDTO data);
    }
}
