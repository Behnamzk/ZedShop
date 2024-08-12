using Microsoft.AspNetCore.Mvc;
using System.IO;
using ZedShop.Core.CustomAuthorization;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Entities;
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
            List<RoleAccessViewModel> roleAccessViews = new List<RoleAccessViewModel>();

            var rolesList = _userService.GetAllRoles();

            foreach(var item in rolesList)
            {
                roleAccessViews.Add(new RoleAccessViewModel() { Id = item.Id, Name = item.DisplayName });
            }

            return View(roleAccessViews);
        }

        [OwnerFilter] // just owner of site
        [Route("/Admin/ManageRoles/EditRole/{roleId}")]
        [HttpGet]
        public IActionResult EditRole(int roleId)
        {
            RoleAccessViewModel roleAccess = new RoleAccessViewModel();

            var role = _userService.GetRoleById(roleId);

            if (role != null)
            {
                roleAccess.Name = role.DisplayName;
                roleAccess.Id = role.Id;
            }

            return View(roleAccess);
        }


        [OwnerFilter] // just owner of site
        [Route("/Admin/ManageRoles/EditRole/{roleId}")]
        [HttpPost]
        public IActionResult EditRole(RoleAccessViewModel roleAccess)
        {
            if (!ModelState.IsValid)
            {

                return View(roleAccess);
            }

            Role role = _userService.GetRoleById(roleAccess.Id);

            if (!(role.DisplayName != roleAccess.Name && _userService.IsRoleNameExist(roleAccess.Name)))
            {
                role.DisplayName = roleAccess.Name;

                _userService.UpdateRole(role);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "نام نقش تکراری است!!");
            }
            return View(roleAccess);

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
                roleAccess.Name = role.DisplayName;
                roleAccess.Id = role.Id;


                foreach (var item in accesss)
                {
                    AccessViewModel accessVM = new AccessViewModel();
                    accessVM.Name = item.DisplayName;
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
            Role role = _userService.GetRoleById(roleAccess.Id);

            ICollection<RoleAccess> roleAccesses = new HashSet<RoleAccess>();


            foreach (var item in roleAccess.Access)
            {
                if (item.IsActive)
                {
                    roleAccesses.Add(new RoleAccess() { AccessId = item.Id, RoleId = roleAccess.Id });
                }
            }

            role.RoleAccesses = roleAccesses;

            _userService.UpdateRole(role);


            return RedirectToAction("Index");

        }

        [OwnerFilter] // just owner of site
        [HttpGet]
        public ActionResult DeleteRole(int id)
        {
            if (_userService.DeleteRole(id)){
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
            
        }
    }
}
