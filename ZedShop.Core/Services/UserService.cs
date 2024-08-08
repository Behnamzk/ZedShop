using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.Convertors;
using ZedShop.Core.DTOs.Account;
using ZedShop.Core.Generator;
using ZedShop.Core.Security;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class UserService : IUserService
    {

        private readonly ZedShopContext _context;
        public UserService(ZedShopContext context)
        {
            _context = context;
        }

        public bool ActiveAccount(string activeCode)
        {
            //var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);

            //if (user == null || user.IsActive)
            //{
            //    return false;
            //}

            //user.IsActive = true;
            //// change user activation code for security 
            //// to bazyabi kalame obor niazesh darim
            //user.ActiveCode = NameGanarator.GenerateUniqueCode();
            //_context.SaveChanges();

            return true;

        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId;
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(User => User.Email == email);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(User => User.UserName == userName);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(User => User.UserName == userName);
        }

        public User LoginUser(LoginViewModel loginViewModel)
        {
            string password = PasswordHelper.EncodePasswordMd5(loginViewModel.Password);
            string email = FixText.FixEmail(loginViewModel.Email);

            return _context.Users.Where(u => u.IsDelete == false).SingleOrDefault(User => User.Email == email && User.Password == password);

        }

        public bool UpdateUser(User user, IFormFile imgProfile)
        {
            if (user == null) { return false; }


            if (imgProfile != null && imgProfile.IsImage())
            {
                if (user.UserAvatar != "Defult.jpg")
                {
                    string deleteimagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                    if (File.Exists(deleteimagePath))
                    {
                        File.Delete(deleteimagePath);
                    }
                }
                user.UserAvatar = NameGenerator.GenerateUniqueCode() + Path.GetExtension(imgProfile.FileName);

                ImageConvertor imgResizer = new ImageConvertor();
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);

                imgResizer.ResizeImage(imgProfile, thumbPath, 250);
            }

            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Where(u => u.IsDelete == false).ToList();
        }


        public int GetAllUsersCount()
        {
            return _context.Users.Where(u=>u.IsDelete == false).Count();
        }

        public bool BanUser(int userId)
        {
            User user = GetUserById(userId);
            if (user == null)
            {
                return false;
            }

            user.IsBan = !user.IsBan;

            _context.Update(user);
            _context.SaveChanges();

            return user.IsBan;

        }

        public bool DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            if (user == null)
            {
                return false;
            }

            user.IsDelete = !user.IsDelete;

            _context.Update(user);
            _context.SaveChanges();

            return user.IsDelete;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == userId);
        }
        public User GetUserByIdWithRole(int userId)
        {
            return _context.Users.Include(u=>u.Role).SingleOrDefault(u => u.UserId == userId);
        }

        public List<Role> GetAllRoles()
        {
            return _context.Roles.ToList();
        }

        public bool IsRoleExist(int roleId)
        {
            return _context.Roles.Any(r => r.Id == roleId);
        }

        public List<User> GetAllUsersPagedRole(int page, int pageSize, int roleId, int filterId) 
        {
            if (roleId == -1)
            {
                switch (filterId){
                    case -1:
                        return _context.Users.Where(u => u.IsDelete == false).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    case 0:
                        return _context.Users.Where(u => u.IsDelete == true).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    case 1:
                        return _context.Users.Where(u => u.IsDelete == false && u.IsBan == true).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    case 2:
                        return _context.Users.Where(u => u.IsDelete == false && u.IsActive == false).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    default:
                        return _context.Users.Where(u => u.IsDelete == false).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                }
            }
            else
            {
                switch (filterId)
                {
                    case -1:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    case 0:
                        return _context.Users.Where(u => u.IsDelete == true  && u.RoleId == roleId).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    case 1:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId && u.IsBan == true).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    case 2:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId && u.IsActive == false).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                    default:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId).Include(u => u.Role).ToPaged(page, pageSize).ToList();

                }

            }
        }

        public int GetAllUsersCount(int roleId, int filterId)
        {
            if (roleId == -1)
            {
                switch (filterId)
                {
                    case -1:
                        return _context.Users.Where(u => u.IsDelete == false).Count();

                    case 0:
                        return _context.Users.Where(u => u.IsDelete == true).Count();

                    case 1:
                        return _context.Users.Where(u => u.IsDelete == false && u.IsBan == true).Count();

                    case 2:
                        return _context.Users.Where(u => u.IsDelete == false && u.IsActive == false).Count();

                    default:
                        return _context.Users.Where(u => u.IsDelete == false).Count();

                }
            }
            else
            {
                switch (filterId)
                {
                    case -1:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId).Count();

                    case 0:
                        return _context.Users.Where(u => u.IsDelete == true && u.RoleId == roleId).Count();

                    case 1:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId && u.IsBan == true).Count();

                    case 2:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId && u.IsActive == false).Count();

                    default:
                        return _context.Users.Where(u => u.IsDelete == false && u.RoleId == roleId).Count();

                }
            }
            
        }

    }
}
