using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using ZedShop.Core.Convertors;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.Services;
using ZedShop.Core.Services.Interface;
using ZedShop.Web.Areas.Admin.Infrastructure;
using ZedShop.Web.Areas.Admin.Models.CommentViewModel;
using ZedShop.Web.Areas.Admin.Models.OpinionViewModel;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SiteOwner")] //Only Admin and SiteOwner
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

            filtersBase.Add(new FilterBaseViewModel(-1, "همه نظر‌ها"));
            filtersBase.Add(new FilterBaseViewModel(0, "مخفی شده"));
            filtersBase.Add(new FilterBaseViewModel(1, "مسدود شده"));

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
            filterId = -1;
            List<OpinionViewModelAdmin> opinionViews = GetOpinions(filterId);

            currentPage = 1;

            ViewBag.Filters = filtersBase;
            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return View(opinionViews);
        }

        private List<OpinionViewModelAdmin> GetOpinions(int _filterId)
        {
            this.filterId = _filterId;
            var opinions = _homeService.GetAllOpinionsPaged(currentPage, numberPerPage, filterId);

            List<OpinionViewModelAdmin> opinionsViews = new List<OpinionViewModelAdmin>();

            foreach (var opinion in opinions)
            {
                var opinionVM = opinionsAEMapper.Map<OpinionViewModelAdmin>(opinion);
                opinionVM.OpinionDate = string.Format("{0}/{1}/{2}", pc.GetYear(opinion.OpinionDate), pc.GetMonth(opinion.OpinionDate), pc.GetDayOfMonth(opinion.OpinionDate));

                opinionsViews.Add(opinionVM);
            }

            allOpinionCount = _homeService.GetAllOpinionsCount(filterId);
            pageCount = (int)Math.Ceiling((double)allOpinionCount / numberPerPage);

            return opinionsViews;
        }

        [HttpGet]
        public IActionResult AllOpinionsOfFilter(int _filterId)
        {
            this.filterId = _filterId;
            List<OpinionViewModelAdmin> OpinionViews = GetOpinions(filterId);

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_OpinionsTable", OpinionViews);
        }

        [HttpGet]
        public IActionResult ChangePage(int _filterId, int pageNumber = 1)
        {
            this.filterId = _filterId;
            currentPage = pageNumber;
            List<OpinionViewModelAdmin> OpinionViews = GetOpinions(filterId);

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_OpinionsTable", OpinionViews);
        }

        [CheckAccess("DeleteOpinion")]
        [Route("/Admin/ManageOpinions/DeleteOpinion")]
        [HttpPost]
        public ActionResult DeleteOpinion(int opinionId)
        {
            bool resault = _homeService.DeleteOpinion(opinionId);

            return Json(new { success = resault });
        }

        [CheckAccess("ShowOpinion")]
        [Route("/Admin/ManageOpinions/ShowOpinion")]
        [HttpPost]
        public ActionResult ShowOpinion(int opinionId)
        {
            if (_homeService.IsOpinionExist(opinionId))
            {
                bool resault = _homeService.ShowOpinion(opinionId);

                return Json(new { resault = resault });
            }

            return RedirectToAction("Index");

        }

        [CheckAccess("BanOpinion")]
        [Route("/Admin/ManageOpinions/BanOpinion")]
        [HttpPost]
        public ActionResult BanOpinion(int opinionId)
        {
            if (_homeService.IsOpinionExist(opinionId))
            {
                bool resault = _homeService.BanOpinion(opinionId);

                return Json(new { resault = resault });
            }

            return RedirectToAction("Index");

        }

    }
}
