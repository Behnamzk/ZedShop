using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Home;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services.Interface
{
    public interface IHomeService
    {
        List<Opinion> GetOpinions();
        List<Opinion> GetOpinions(int count);

        bool AddOpinion(OpinionViewModel opinionViewModel);

        int GetAllOpinionsCount(int filterId);
        List<Opinion> GetAllOpinionsPaged(int page, int pageSize, int filterId);
        bool DeleteOpinion(int opinionId);
        bool ShowOpinion(int opinionId);
        bool IsOpinionExist(int opinionId);
        Opinion GetOpinion(int opinionId);
    }
}
