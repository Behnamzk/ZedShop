using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Home;
using ZedShop.Core.DTOs.Product;
using ZedShop.Core.Services.Interface;
using ZedShop.DataLayer.Context;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Services
{
    public class HomeService : IHomeService
    {
        private readonly ZedShopContext _context;

        public HomeService(ZedShopContext context)
        {
            _context = context;
        }

        public bool AddOpinion(OpinionViewModel opinionViewModel)
        {
            if (opinionViewModel == null)
            {
                return false;
            }

            if (opinionViewModel.UserId == -1)
            {
                return false;
            }

            Opinion opinion = new Opinion()
            {
                OpinionText = opinionViewModel.Content,
                UserId = opinionViewModel.UserId,
                OpinionDate = DateTime.Now,
                IsBan = false

            };

            _context.Opinions.Add(opinion);
            _context.SaveChanges();
            return true;
        }

        public List<Opinion> GetOpinions()
        {
            return _context.Opinions.Where(o=>o.IsBan == false).Include(o => o.User).OrderByDescending(c=>c.OpinionRate).ToList();
        }

        public List<Opinion> GetOpinions(int count)
        {
            return _context.Opinions.Where(o => o.IsBan == false).Include(o=>o.User).OrderByDescending(c => c.OpinionRate).Take(count).ToList();
        }
    }
}
