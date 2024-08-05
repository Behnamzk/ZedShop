using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
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
                    IsBan = user.IsBan

                });

            }

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;

            return View(userViews);
        }


        //[Authorize]
        [Route("/Admin/ManageUsers/EditUserRole/{userId}")]
        [HttpGet]
        public IActionResult EditUserRole(int userId)
        {
            UserRoleViewModel userRole = new UserRoleViewModel();
            var user = _userService.GetUserByIdWithRole(userId);

            if (user != null)
            {
                userRole.UserName = user.UserName;
                userRole.UserId = user.UserId;
                userRole.RoleName = user.Role.Name;
                userRole.RoleId = user.RoleId;
            }

            List<Role> roles = _userService.GetAllRoles();

            ViewBag.Roles = roles;


            return View(userRole);
        }

        //[Authorize]
        [Route("/Admin/ManageUsers/EditUserRole/{userId}")]
        [HttpPost]
        public IActionResult EditUserRole(UserRoleViewModel userRole)
        {
            if (!ModelState.IsValid)
            {

                return View(userRole);
            }

            User user = _userService.GetUserById(userRole.UserId);

            // check user is notnull
            if (user != null)
            {
                // Todo: implement site policy

                if (_userService.IsRoleExist(userRole.SelectedRoleId))
                {
                    user.RoleId = userRole.SelectedRoleId;

                    _userService.UpdateUser(user, null);

                    return RedirectToAction("Index");
                }
            }


            return View(userRole);
        }



        //[Authorize]
        [Route("/Admin/ManageUsers/EditUser/{userId}")]
        [HttpGet]
        public IActionResult EditUser(int userId)
        {

            var user = _userService.GetUserById(userId);

            UserViewModel userViewModel = new UserViewModel();

            if (user != null)
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
        [Route("/Admin/ManageUsers/EditUser/{userId}")]
        [HttpPost]
        public IActionResult EditUser(UserViewModel userViewModel)
        {

            if (!ModelState.IsValid)
            {

                return View(userViewModel);
            }

            User user = _userService.GetUserById(userViewModel.UserId);

            // check user is notnull
            if (user != null)
            {

                // check Email is changed and already exist
                if (!(!user.Email.Equals(userViewModel.Email) && _userService.IsExistEmail(user.Email)))
                {

                    // check UserName is changed and already exist
                    if (!(!user.UserName.Equals(userViewModel.UserName) && _userService.IsExistUserName(user.UserName)))
                    {
                        user.Email = userViewModel.Email;
                        user.UserName = userViewModel.UserName;
                        user.IsActive = userViewModel.IsActive;
                        user.IsBan = userViewModel.IsBan;

                        _userService.UpdateUser(user, userViewModel.ProfileFile);

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "نام کاربری تکراری است!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "ایمیل تکراری است!!");

                }

            }
            else
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات فوق یافت نشد");
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
