using AutoMapper;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.OpinionViewModel;

namespace ZedShop.Web.Areas.Admin.Infrastructure
{
    public class OpinionMapping:Profile
    {
        public OpinionMapping()
        {
            CreateMap<Opinion, OpinionViewModelAdmin>()
               .ForMember(c => c.UserName, o => o.MapFrom(com => com.User.UserName)).ReverseMap();
        }

    }
}
