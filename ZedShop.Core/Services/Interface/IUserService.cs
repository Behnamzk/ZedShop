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

        public List<User> GetAllUsers();
        public int GetAllUsersCount();
        public List<User> GetAllUsersPaged(int page, int pageSize);

        public bool UpdateUser(User user, IFormFile imgProfile);

    }
}
