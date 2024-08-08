using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZedShop.Core.Services.Interface;
using System.Security.Claims;
using ZedShop.DataLayer.Entities;


namespace ZedShop.Core.CustomAuthorization
{


    public class OwnerFilter : Attribute, IAuthorizationFilter
    {
        // simple sample of filtering
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Check if the user has the required role
            if (context.HttpContext.User.Identity != null)
            {
                var claims = context.HttpContext.User.Claims.ToList();
                var role_id = claims[2].Value; // get role attribute

                if(role_id != null)
                {
                    if(role_id != "3")
                    {
                        context.Result = new RedirectToActionResult("Index", "ManageUsers", null);
                        return;
                    }
                }

            }

        }
    }

}
