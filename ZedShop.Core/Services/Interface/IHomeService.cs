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
    }
}
