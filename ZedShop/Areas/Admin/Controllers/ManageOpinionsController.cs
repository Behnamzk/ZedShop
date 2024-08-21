using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ZedShop.Core.Convertors;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.Web.Areas.Admin.Infrastructure;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    public class ManageOpinionsController : Controller
    {
        private readonly IHomeService _homeService;

        private int pageCount, currentPage, allOpinionCount, numberPerPage, filterId;

        private readonly List<FilterBaseViewModel> filtersBase = new List<FilterBaseViewModel>();

        private readonly PersianCalendar pc;

        private readonly MapperConfiguration opinionsAEMapperConfig;

        private IMapper opinionsAEMapper;

        public ManageOpinionsController(IHomeService homeService)
        {
            this._homeService = homeService;

            filtersBase.Add(new FilterBaseViewModel(-1, "همه دیدکاه‌ها"));
            filtersBase.Add(new FilterBaseViewModel(0, "مخفی شده"));

            pc = new PersianCalendar();


            opinionsAEMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<OpinionMapping>());
            opinionsAEMapper = opinionsAEMapperConfig.CreateMapper();

            // paging initialization
            numberPerPage = 5;
            currentPage = 1;
            filterId = -1;
            allOpinionCount = _homeService.GetAllOpinionsCount(filterId);
            pageCount = (int)Math.Ceiling((double)allOpinionCount / numberPerPage);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
