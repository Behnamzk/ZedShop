using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZedShop.Core.Convertors;
using ZedShop.Core.DTOs;
using ZedShop.Core.Generator;
using ZedShop.Core.Security;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Web.Controllers.Account
{
    public class AccountController : Controller
    {
        private IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _service.LoginUser(loginViewModel);

            if ( user != null)
            {
                if (user.IsActive)
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var peroperties = new AuthenticationProperties
                    {
                        IsPersistent = loginViewModel.RememberMe
                    };

                    // dastori ke karbar ro login mikone ine
                    HttpContext.SignInAsync(principal, peroperties);

                    return Redirect("/");

                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی‌باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View();

        }


        #endregion

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            // check input is valid or not
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            // check input is valid or not
            if (_service.IsExistUserName(registerViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی‌باشد");
                return View(registerViewModel);
            }

            // check Email is valid
            if (_service.IsExistEmail(registerViewModel.Email))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی‌باشد");
                return View(registerViewModel);
            }

            GenderTypes genderType = (GenderTypes)Enum.Parse(typeof(GenderTypes), registerViewModel.Gender, true);

            var type = genderType;

            User user = new User
            {
                ActiveCode = NameGanarator.GenerateUniqueCode(),
                Email = FixText.FixEmail(registerViewModel.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(registerViewModel.Password),
                RoleId = 1,
                gender = genderType,
                UserAvatar = "Defult.jpg",
                UserName = registerViewModel.UserName,
                RegisterDate = DateTime.Now,

            };

            _service.AddUser(user);

            // Todo Send activation email

            return View("SuccessRegister", user);
        }

        #endregion


        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

    }
}
