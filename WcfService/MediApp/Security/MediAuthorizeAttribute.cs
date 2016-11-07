using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace MediApp.Security
{
    public class MediAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionPersister.Username))
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    (new { controller = "Account", action = "Login" }));
            else
            {
                RolePrincipal rp = new RolePrincipal(SessionPersister.Username);
                if (!rp.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    (new { controller = "Account", action = "AccessDenied" }));
                }

            }
        }
    }
}
