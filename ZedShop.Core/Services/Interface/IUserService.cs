using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Account;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services.Interface
{
    public interface IUserService
    {
        public bool IsExistUserName(string userName);

        public bool IsExistEmail(string email);

        public int AddUser(User user);

        public User LoginUser(LoginViewModel loginViewModel);

        public bool ActiveAccount(string activeCode);

        public User GetUserByUserName(string userName);
        public User GetUserById(int userId);
        public User GetUserByIdWithRole(int userId);
        public bool BanUser(int userId);
        public bool DeleteUser(int userId);

        public List<User> GetAllUsers();

        public int GetAllUsersCount(int roleId, int filterId);
        public int GetAllUsersCount();
        public List<User> GetAllUsersPagedRole(int page, int pageSize, int roleId, int filterId);

        public bool UpdateUser(User user, IFormFile imgProfile);

        #region Role
        public List<Role> GetAllRoles();
        public bool IsRoleExist(int roleId);
        public bool IsRoleNameExist(string roleName);
        public bool IsRoleDisplayNameExist(string roleName);
        public bool AddRole(Role role);
        public bool DeleteRole(int roleId);

        public Role GetRoleById(int roleId);

        public List<int> GetRoleAccessIds(int roleId);

        public bool UpdateRole(Role role);
        #endregion

        #region Access
        public List<Access> GetAllAccesses();
        public bool IsAccessExist(int accessId);
        public Access GetAccessById(int accessId);
        public List<Access> GetRolesAccess(int roleId);
        #endregion
    }
}
