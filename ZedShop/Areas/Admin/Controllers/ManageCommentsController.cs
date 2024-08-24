using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Globalization;
using ZedShop.Core.Convertors;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.CommentViewModel;
using ZedShop.Web.Areas.Admin.Models.ProductViewModel;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SiteOwner")] //Only Admin and SiteOwner
    public class ManageCommentsController : Controller
    {
        private readonly IProductService _productService;

        private int pageCount, currentPage, allCommentCount, numberPerPage, filterId;

        private readonly List<FilterBaseViewModel> filtersBase = new List<FilterBaseViewModel>();

        private readonly PersianCalendar pc;

        private readonly MapperConfiguration productAEMapperConfig;

        private IMapper productAEMapper;
        public ManageCommentsController(IProductService productService)
        {
            _productService = productService;

            filtersBase.Add(new FilterBaseViewModel(-1, "همه دیدگاه‌ها"));
            filtersBase.Add(new FilterBaseViewModel(0, "مخفی شده"));

            pc = new PersianCalendar();


            productAEMapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<ProductMapping>());
            productAEMapper = productAEMapperConfig.CreateMapper();

            // paging initialization
            numberPerPage = 5;
            currentPage = 1;
            filterId = -1;
            allCommentCount = _productService.GetAllCommentsCount(filterId);
            pageCount = (int)Math.Ceiling((double)allCommentCount / numberPerPage);
        }
        public IActionResult Index()
        {
            filterId = -1;
            List<CommentViewModelAdmin> commentsViews = GetComments(filterId);


            currentPage = 1;

            ViewBag.Filters = filtersBase;
            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return View(commentsViews);
        }

        private List<CommentViewModelAdmin> GetComments(int _filterId)
        {
            this.filterId = _filterId;
            var comments = _productService.GetAllCommemtsPaged(currentPage, numberPerPage, filterId);

            List<CommentViewModelAdmin> commentsViews = new List<CommentViewModelAdmin>();

            foreach (var comment in comments)
            {
                var commentVM = productAEMapper.Map<CommentViewModelAdmin>(comment);
                commentVM.CommentDate = string.Format("{0}/{1}/{2}", pc.GetYear(comment.CommentDate), pc.GetMonth(comment.CommentDate), pc.GetDayOfMonth(comment.CommentDate));
                
                commentsViews.Add(commentVM);
            }

            allCommentCount = _productService.GetAllCommentsCount(filterId);
            pageCount = (int)Math.Ceiling((double)allCommentCount / numberPerPage);

            return commentsViews;
        }

        [HttpGet]
        public IActionResult AllCommentsOfFilter(int _filterId)
        {
            this.filterId = _filterId;
            List<CommentViewModelAdmin> commentViews = GetComments(filterId);

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_CommentsTable", commentViews);
        }

        [HttpGet]
        public IActionResult ChangePage(int _filterId, int pageNumber = 1)
        {
            this.filterId = _filterId;
            currentPage = pageNumber;
            List<CommentViewModelAdmin> commentViews = GetComments(filterId);

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.FilterId = filterId;

            return PartialView("_CommentsTable", commentViews);
        }

        [CheckAccess("DeleteComment")]
        [Route("/Admin/ManageComments/DeleteComment")]
        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            bool resault = _productService.DeleteComment(commentId);

            return Json(new { success = resault });
        }

        [CheckAccess("ShowComment")]
        [Route("/Admin/ManageComments/ShowComment")]
        [HttpPost]
        public ActionResult ShowComment(int commentId)
        {
            if (_productService.IsCommentExist(commentId))
            {
                bool resault = _productService.ShowComment(commentId);

                return Json(new { resault = resault });
            }

            return RedirectToAction("Index");

        }

    }
}
