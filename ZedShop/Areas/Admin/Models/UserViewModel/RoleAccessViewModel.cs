using System.ComponentModel.DataAnnotations;

namespace ZedShop.Web.Areas.Admin.Models.UserViewModel
{
    public class RoleAccessViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<AccessViewModel> Access { get; set; } = new List<AccessViewModel>();
    }

    public class AccessViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }= string.Empty;

        public bool IsActive { get; set; } = false;
    }
}
