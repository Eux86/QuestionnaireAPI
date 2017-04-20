﻿using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Questionnaire.Api.Identity
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            //var user = context.OwinContext.Get<BooksContext>().Users.FirstOrDefault(u => u.UserName == context.UserName);
            //if (!context.OwinContext.Get<BookUserManager>().CheckPassword(user, context.Password))
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect");
            //    context.Rejected();
            //    return Task.FromResult<object>(null);
            //}

            if (context.UserName != "euxAdmin" || context.Password != "1492222")
            {
                context.SetError("invalid_grant", "The user name or password is incorrect");
                context.Rejected();
                return Task.FromResult<object>(null);
            }


            ClaimsIdentity identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            //var ticket = new AuthenticationTicket(SetClaimsIdentity(context, user), new AuthenticationProperties());
            context.Validated(ticket);

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        //private static ClaimsIdentity SetClaimsIdentity(OAuthGrantResourceOwnerCredentialsContext context, IdentityUser user)
        //{
        //    var identity = new ClaimsIdentity("JWT");
        //    identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
        //    identity.AddClaim(new Claim("sub", context.UserName));

        //    var userRoles = context.OwinContext.Get<BookUserManager>().GetRoles(user.Id);
        //    foreach (var role in userRoles)
        //    {
        //        identity.AddClaim(new Claim(ClaimTypes.Role, role));
        //    }

        //    return identity;
        //}
    }
}