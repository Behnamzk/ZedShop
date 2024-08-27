using AutoMapper;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.CommentViewModel;
using ZedShop.Web.Areas.Admin.Models.ProductViewModel;


namespace ZedShop.Core.Convertors
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductAEViewModelAdmin>().ReverseMap();
            
            CreateMap<ProductAEViewModelAdmin, Product>().ReverseMap();


            CreateMap<Comment, CommentViewModelAdmin>()
                .ForMember(c=>c.ProductName, o=>o.MapFrom(com=>com.Product.Name))
                .ForMember(c => c.UserName, o => o.MapFrom(com => com.User.UserName)).ReverseMap();

        }


    }
}
