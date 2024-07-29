using Microsoft.AspNetCore.Authorization;
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
            numberPerPage = 10;
            currentPage = 1;
            allUserCount = userService.GetAllUsers().Count();
            pageCount = (int)Math.Ceiling((double)allUserCount / numberPerPage);

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

        //[Authorize]
        [Route("/Admin/ManageUsers/EditUser/{userId}")]
        public IActionResult EditUser(int userId)
        {

            var user = _userService.GetUserById(userId);

            UserViewModel userViewModel = new UserViewModel();

            if(user != null)
            {
                userViewModel.UserId = userId;
                userViewModel.UserName = user.UserName;
                userViewModel.Email = user.Email;
                userViewModel.Password = user.Password;
                userViewModel.UserAvatar = user.UserAvatar;
                userViewModel.Gender = user.gender;
                userViewModel.IsActive = user.IsActive;
                userViewModel.IsBan = user.IsBan;

            }


            return View(userViewModel);
        }

        //[Authorize]
        [Route("/Admin/ManageUsers/BanUser/{userId}")]
        [HttpGet]
        public ActionResult BanUser(int userId)
        {
            bool resault = _userService.BanUser(userId);

            return Json(new { resault = resault });
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            return Json(new { success = true });
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
