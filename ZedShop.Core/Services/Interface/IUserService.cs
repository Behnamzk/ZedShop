using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services.Interface
{
    public interface IUserService
    {
        public bool IsExistUserName(string userName);

        public bool IsExistEmail(string email);

        public int AddUser(User user);

        public User LoginUser(LoginViewModel loginViewModel);

        public bool ActiveAccount(string ActiveCode);

        public User GetUserByUserName(string user_name);

    }
}
