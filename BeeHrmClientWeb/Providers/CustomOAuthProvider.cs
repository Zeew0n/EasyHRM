using BeeHRM.ApplicationService.Implementations;
using BeeHrmClientWeb.Authentication;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BeeHrmClientWeb.Providers
{
    public class CustomOAuthProvider: OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ///Author:Sushil Ghimire
            ///Purpose:Used To Enable CORS for Authorization Server and End Point /token
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            #region Previous Code : Uses Identity to validate user

            //var userManager=HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);


            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}

            ////if (!user.EmailConfirmed)
            ////{
            ////    context.SetError("invalid_grant", "User did not confirm email.");
            ////    return;
            ////}

            //ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, "JWT");

            //var ticket = new AuthenticationTicket(oAuthIdentity, null);

            //context.Validated(ticket);
            #endregion


            #region Domain Authentication
            //Author: Ram K Thapa

            string domainName = context.UserName.Split('@')[1] == null ? "" : context.UserName.Split('@')[1].ToString();

            string adPath = string.Concat("LDAP://", domainName);

            LdapAuthentication adAuth = new LdapAuthentication(adPath);
            try
            {
                if (false == adAuth.IsAuthenticated("spi", context.UserName, context.Password))
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
            }
            #endregion

            #region Retrieve User Information and Add to Claim
            string UserRoleId = string.Empty, UserName = string.Empty;

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            var userInfo = UserInfoService.Instance.GetUserInfoByEmail(context.UserName);
            if (userInfo != null)
            {
                UserRoleId = userInfo.UsersUserRoleRels == null ? "" : userInfo.UsersUserRoleRels.FirstOrDefault().UserRole.UserRoleID.ToString();
                UserName = userInfo.UserName;
            }
            identity.AddClaim(new Claim(ClaimTypes.Email, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, UserRoleId));
            identity.AddClaim(new Claim(ClaimTypes.Name, UserName));

            #endregion


            context.Validated(identity);


        }



    }
}