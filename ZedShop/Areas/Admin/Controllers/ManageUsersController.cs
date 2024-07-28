using Microsoft.AspNetCore.Mvc;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles ="Admin , Owner")]
    public class ManageUsersController : Controller
    {
        private readonly IUserService _userService;

        private int pageCount, currentPage, allUserCount, numberPerPage;



        public ManageUsersController(IUserService userService)
        {
            _userService = userService;

            // paging initialization
            numberPerPage = 1;
            currentPage = 1;
            allUserCount = userService.GetAllUsers().Count();
            pageCount = allUserCount / numberPerPage;

        }


        public IActionResult Index()
        {

            var users = _userService.GetAllUsersPaged(currentPage, numberPerPage);

            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                userViews.Add(new UserViewModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Gender = user.gender,
                    IsActive = user.IsActive,
                    IsBan = false

                });

            }

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;

            return View(userViews);
        }

        [HttpGet]
        public IActionResult ChangePage(int pageNumber = 1)
        {
            var users = _userService.GetAllUsersPaged(pageNumber, numberPerPage);

            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                userViews.Add(new UserViewModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    Gender = user.gender,
                    IsActive = user.IsActive,
                    IsBan = false

                });

            }

            currentPage = pageNumber;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            return PartialView("_UsersTable", userViews);
        }
    }
}
