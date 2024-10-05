using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedShop.Core.DTOs.Product;
using ZedShop.DataLayer.Entities;

namespace ZedShop.Core.Convertors
{
    public class ZedMapping : Profile
    {
        public ZedMapping() { 
            CreateMap<Product, OrderDetailProductsViewModel>().ReverseMap();
        }
    }
}
