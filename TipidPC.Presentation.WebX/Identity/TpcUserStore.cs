using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TipidPC.Domain.Models;
using TipidPC.Infrastructure.Persistence;
using TipidPC.Presentation.Web.Models;
using System.Security.Claims;

namespace TipidPC.Presentation.Web.Identity
{
    public class TpcUserStore :
        IUserStore<TpcApplicationUser, int>,
        IUserPasswordStore<TpcApplicationUser, int>,
        IUserEmailStore<TpcApplicationUser, int>,
        IUserClaimStore<TpcApplicationUser, int>,
        IQueryableUserStore<TpcApplicationUser, int>
    {
        // Fields
        private ITpcDbContext<TpcApplicationUser> _context;

        // Properties
        public IQueryable<TpcApplicationUser> Users
        {
            get
            {
                return _context.Users;
            }
        }

        // Constructors
        public TpcUserStore(ITpcDbContext<TpcApplicationUser> context)
        {
            _context = context;
        }

        // Method Implementation for IUserStore
        public void Dispose()
        {
            _context.Dispose();
        }
        public Task CreateAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.UserName = user.UserName.Trim();
            _context.Set<TpcApplicationUser>().Add(user);
            return Task.FromResult<object>(null);
        }
        public Task DeleteAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            _context.Set<TpcApplicationUser>().Remove(user);
            return Task.FromResult<object>(null);
        }
        public Task<TpcApplicationUser> FindByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("Out of range.", "userId");
            }

            return _context
                .Set<TpcApplicationUser>()
                .FindAsync(userId);

            //TpcApplicationUser user;
            //if ((user = _context
            //    .Set<TpcApplicationUser>()
            //    .Find(userId)) != null)
            //{
            //    return Task.FromResult<TpcApplicationUser>(user);
            //}

            //return Task.FromResult<TpcApplicationUser>(null);
        }
        public Task<TpcApplicationUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName.Trim()))
            {
                throw new ArgumentException("Null or empty.", "userName");
            }

            return _context
                .Set<TpcApplicationUser>()
                .SingleOrDefaultAsync(u => u.UserName == userName.Trim().ToLower());

            //TpcApplicationUser tpcUser;
            //if ((tpcUser = _context
            //    .Set<TpcApplicationUser>()
            //    .SingleOrDefaultAsync(u => u.UserName == userName.Trim().ToLower())) != null)
            //{
            //    return Task.FromResult<TpcApplicationUser>(tpcUser); 
            //}

            //return Task.FromResult<TpcApplicationUser>(null);
        }
        public Task UpdateAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (_context.Entry<TpcApplicationUser>(user).State == EntityState.Detached)
            {
                _context.Set<TpcApplicationUser>().Attach(user);
            }
            _context.Entry(user).State = EntityState.Modified;
            return Task.FromResult<object>(null);
        }

        // Method Implementation for IUserPasswordStore
        public Task<string> GetPasswordHashAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var tpcUser = _context
                .Set<TpcApplicationUser>()
                .Find(user.Id);

            return Task.FromResult<string>(tpcUser.Password);
        }
        public Task<bool> HasPasswordAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var tpcUser = _context
                .Set<TpcApplicationUser>()
                .Find(user.Id);
            var hasPassword = !string.IsNullOrEmpty(tpcUser.Password);

            return Task.FromResult<bool>(Boolean.Parse(hasPassword.ToString()));
        }
        public Task SetPasswordHashAsync(TpcApplicationUser user, string passwordHash)
        {
            if (user != null)
            {
                user.Password = passwordHash.Trim(); 
            }

            return Task.FromResult<object>(null);
        }

        // Method Implementation for IUserEmailStore
        public Task SetEmailAsync(TpcApplicationUser user, string email)
        {
            user.Email = email.Trim().ToLower();
            return Task.FromResult<object>(null);
        }
        public Task<string> GetEmailAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            TpcApplicationUser tpcUser;
            if ((tpcUser = _context
                .Set<TpcApplicationUser>()
                .Find(user.Id)) != null)
            {
                return Task.FromResult<string>(tpcUser.Email);
            }

            return Task.FromResult<string>(null);
        }
        public Task<bool> GetEmailConfirmedAsync(TpcApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            TpcApplicationUser tpcUser;
            if ((tpcUser = _context
                .Set<TpcApplicationUser>()
                .Find(user.Id)) != null)
            {
                return Task.FromResult<bool>(tpcUser.IsEmailConfirmed);
            }

            return Task.FromResult<bool>(false);
        }
        public Task SetEmailConfirmedAsync(TpcApplicationUser user, bool confirmed)
        {
            if (user != null)
            {
                user.IsEmailConfirmed = confirmed; 
            }

            return Task.FromResult<object>(null);
        }
        public Task<TpcApplicationUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email.Trim()))
            {
                throw new ArgumentNullException("email");
            }

            return _context
                .Set<TpcApplicationUser>()
                .SingleOrDefaultAsync(t => string.Equals(t.Email, email.Trim()));
        }


        public Task<IList<Claim>> GetClaimsAsync(TpcApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task AddClaimAsync(TpcApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(TpcApplicationUser user, Claim claim)
        {
            throw new NotImplementedException();
        }
    }
}