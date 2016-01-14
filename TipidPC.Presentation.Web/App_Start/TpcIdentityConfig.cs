using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using TipidPC.Infrastructure.Persistence;
using TipidPC.Presentation.Web.Identity;
using TipidPC.Presentation.Web.Models;

namespace TipidPC.Presentation.Web
{
    public class TpcApplicationUserManager : TpcUserManager<TpcApplicationUser>
    {
        public TpcApplicationUserManager(IUserStore<TpcApplicationUser, int> store)
            : base(store)
        {
        }
        public static TpcApplicationUserManager Create(IdentityFactoryOptions<TpcApplicationUserManager> options, IOwinContext context)
        {
            var manager = new TpcApplicationUserManager(new TpcUserStore(context.Get<TpcApplicationDbContext>() as ITpcDbContext<TpcApplicationUser>));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<TpcApplicationUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<TpcApplicationUser, int>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<TpcApplicationUser, int>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<TpcApplicationUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }        
    }
    public class TpcApplicationSignInManager : SignInManager<TpcApplicationUser, int>
    {
        public TpcApplicationSignInManager(TpcApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(TpcApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((TpcApplicationUserManager)UserManager);
        }

        public static TpcApplicationSignInManager Create(IdentityFactoryOptions<TpcApplicationSignInManager> options, IOwinContext context)
        {
            return new TpcApplicationSignInManager(context.GetUserManager<TpcApplicationUserManager>(), context.Authentication);
        }
    }
}