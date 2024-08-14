using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ZedShop.Core.CustomAuthorization
{
    public class ManangeUsersFilter : Attribute, IAuthorizationFilter
    {
        private string _actionRequrment;

        public ManangeUsersFilter(string actionRequrment) { 
            _actionRequrment = actionRequrment;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            // Check if the user has the required role
            if (context.HttpContext.User.Identity != null)
            {
                var claims = context.HttpContext.User.Claims.ToList();

                List<string> userAccess = new List<string>();

                if (claims[3].Value != null)
                {
                    userAccess = JsonSerializer.Deserialize<List<string>>(claims[3].Value);

                }
                var role_id = claims[2].Value; // get role attribute

                if (role_id == null || userAccess == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                    return;
                }

                if (userAccess.Count() == 0 || !userAccess.Contains(_actionRequrment))
                {
                    context.Result = new RedirectToActionResult("Index", "ManageUsers", null);
                    return;
                }


            }

        }
    }
}
