using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services;
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

        private int pageCount, currentPage, allUserCount, numberPerPage, roleId;



        public ManageUsersController(IUserService userService)
        {
            _userService = userService;

            // paging initialization

            numberPerPage = 10;
            currentPage = 1;
            allUserCount = userService.GetAllUsersCount(roleId);
            pageCount = (int)Math.Ceiling((double)allUserCount / numberPerPage);

        }


        public IActionResult Index()
        {
            roleId = -1;
            List<UserViewModel> userViews = GetUsers(roleId);

            var roles = _userService.GetAllRoles();
            ViewBag.Roles = roles;

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.RoleId = roleId;

            return View(userViews);
        }

        [HttpGet]
        public IActionResult AllUsersOfRole(int _roleId)
        {
            
            List<UserViewModel> userViews = GetUsers(_roleId);

            currentPage = 1;

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.RoleId = roleId;

            return PartialView("_UsersTable", userViews);
        }

        public List<UserViewModel> GetUsers(int _roleId)
        {
            this.roleId = _roleId;
            var users = _userService.GetAllUsersPagedRole(currentPage, numberPerPage, roleId);

            List<UserViewModel> userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                userViews.Add(new UserViewModel
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    RoleName = user.Role.Name,
                    Email = user.Email,
                    Gender = user.gender,
                    IsActive = user.IsActive,
                    IsBan = user.IsBan
                });
            }

            allUserCount = _userService.GetAllUsersCount(roleId);
            pageCount = (int)Math.Ceiling((double)allUserCount / numberPerPage);

            return userViews;
        }

        [Authorize]
        [Route("/Admin/ManageUsers/EditUserRole/{userId}")]
        [HttpGet]
        public IActionResult EditUserRole(int userId)
        {
            if(User.Identity != null)
            {
                var username = User.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    User currentUser = _userService.GetUserByUserName(username);

                    if(currentUser.RoleId == 3) // Site Owner
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

                }
            }

            return RedirectToAction("Index");


        }

        [Authorize]
        [Route("/Admin/ManageUsers/EditUserRole/{userId}")]
        [HttpPost]
        public IActionResult EditUserRole(UserRoleViewModel userRole)
        {
            if (User.Identity != null)
            {
                var username = User.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    User currentUser = _userService.GetUserByUserName(username);

                    if (currentUser.RoleId == 3) // Site Owner
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
                }
            }
            return RedirectToAction("Index");
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
        public IActionResult ChangePage(int _roleId, int pageNumber = 1)
        {
            this.roleId = _roleId;
            currentPage = pageNumber;
            List<UserViewModel> userViews = GetUsers(roleId);

            ViewBag.NumberOfPage = pageCount;
            ViewBag.CurrentPage = currentPage;
            ViewBag.RoleId = roleId;
            return PartialView("_UsersTable", userViews);
        }
    }
}
