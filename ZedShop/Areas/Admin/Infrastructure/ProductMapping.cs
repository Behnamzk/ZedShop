using AutoMapper;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.DataLayer.Entities;
using ZedShop.Web.Areas.Admin.Models.ProductViewModel;


namespace ZedShop.Core.Convertors
{
    public class ProductMapping:Profile
    {
        private readonly PersianCalendar pc = new PersianCalendar();
        public ProductMapping()
        {
            CreateMap<Product, ProductAEViewModelAdmin>().ReverseMap();
            
            CreateMap<ProductAEViewModelAdmin, Product>().ReverseMap();
        }


    }
}
