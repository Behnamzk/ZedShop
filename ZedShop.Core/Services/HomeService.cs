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

        public bool DeleteOpinion(int opinionId)
        {
            throw new NotImplementedException();
        }

        public int GetAllOpinionsCount(int filterId)
        {
            switch (filterId)
            {
                case -1:
                    // All products except Deleted products
                    return _context.Opinions.Count();
                case 0:
                    // IsShow == false
                    return _context.Opinions.Where(u => u.IsShow == false).Count();
                case 1:
                    // IsBan == true
                    return _context.Opinions.Where(u => u.IsBan == true).Count();

                default:
                    return _context.Opinions.Count();

            }
        }

        public List<Opinion> GetAllOpinionsPaged(int page, int pageSize, int filterId)
        {
            throw new NotImplementedException();
        }

        public Opinion GetOpinion(int opinionId)
        {
            return _context.Opinions.Include(o => o.User).SingleOrDefault(o => o.Id == opinionId);

        }

        public List<Opinion> GetOpinions()
        {
            return _context.Opinions.Where(o=>o.IsBan == false).Include(o => o.User).OrderByDescending(c=>c.OpinionRate).ToList();
        }

        public List<Opinion> GetOpinions(int count)
        {
            return _context.Opinions.Where(o => o.IsBan == false).Include(o=>o.User).OrderByDescending(c => c.OpinionRate).Take(count).ToList();
        }

        public bool IsOpinionExist(int opinionId)
        {
            return _context.Opinions.Any(o => o.Id == opinionId);
        }

        public bool ShowOpinion(int opinionId)
        {
            var opinion = GetOpinion(opinionId);

            opinion.IsShow = !opinion.IsShow;
            _context.Opinions.Update(opinion);
            _context.SaveChanges();
            return opinion.IsShow;
        }
    }
}
