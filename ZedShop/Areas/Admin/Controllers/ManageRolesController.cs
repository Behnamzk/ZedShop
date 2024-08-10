using Microsoft.AspNetCore.Mvc;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.Services.Interface;
using ZedShop.Web.Areas.Admin.Models.UserViewModel;

namespace ZedShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageRolesController : Controller
    {
        private readonly IUserService _userService;

        public ManageRolesController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [OwnerFilter] // just owner of site
        [Route("/Admin/ManageRoles/EditRoleAccess/{roleId}")]
        [HttpGet]
        public IActionResult EditRoleAccess(int roleId)
        {
            RoleAccessViewModel roleAccess = new RoleAccessViewModel();

            var accesss = _userService.GetAllAccesses();

            var role = _userService.GetRoleById(roleId);

            if (role != null)
            {
                roleAccess.Name = role.Name;
                roleAccess.Id = role.Id;


                foreach (var item in accesss)
                {
                    AccessViewModel accessVM = new AccessViewModel();
                    accessVM.Name = item.Name;
                    accessVM.Id = item.Id;
                    accessVM.IsActive = false;

                    if (role.RoleAccesses.Select(x => x.AccessId).Contains(item.Id))
                    {
                        accessVM.IsActive = true;
                    }
                    roleAccess.Access.Add(accessVM);
                }
            }

            return View(roleAccess);
        }

        [OwnerFilter] // just owner of site
        [Route("/Admin/ManageRoles/EditRoleAccess/{roleId}")]
        [HttpPost]
        public IActionResult EditRoleAccess(RoleAccessViewModel roleAccess)
        {
            return RedirectToAction("Index");

        }
    }
}
