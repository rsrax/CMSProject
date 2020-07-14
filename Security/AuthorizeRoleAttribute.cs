using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMSProject.Models.DB;
using CMSProject.Models.EntityManager;

namespace CMSProject.Security
{
	public class AuthorizeRolesAttribute : AuthorizeAttribute
	{
		private readonly string[] userAssignedRoles;
		public AuthorizeRolesAttribute(params string[] roles)
		{
			this.userAssignedRoles = roles;
		}
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			bool authorize = false;
			using (CMSProjectEntities db = new CMSProjectEntities())
			{
				UserManager UM = new UserManager();
				foreach (var roles in userAssignedRoles)
				{
					authorize = UM.IsUserInRole(httpContext.User.Identity.Name, roles);
					if (authorize)
						return authorize;
				}
			}
			return authorize;
		}
		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new RedirectResult("~/Home/UnAuthorized");
		}
	}
}