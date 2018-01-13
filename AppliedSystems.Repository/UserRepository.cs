using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;
using AppliedSystems.Domain.DAO;
using Microsoft.AspNet.Identity;

namespace AppliedSystems.Repository
{
    [ExcludeFromCodeCoverage]
    public sealed class UserRepository : 
        IUserEmailStore<User, int>, 
        IUserLockoutStore<User, int>, 
        IUserPasswordStore<User, int>, 
        IUserTwoFactorStore<User, int>, 
        IUserClaimStore<User, int>
    {
        private readonly AppliedSystemsContext _context;

        public UserRepository(AppliedSystemsContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public Task CreateAsync(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            // TODO: Double check nothing else needs to be done here
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return _context.Users.FindAsync(userId);
        }

        public Task<User> FindByNameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public Task SetEmailAsync(User user, string email)
        {
            user.Email = email;
            return _context.SaveChangesAsync();
        }

        public Task<string> GetEmailAsync(User user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user)
        {
            // TODO: Update this to implement emeail confirmation if required
            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed)
        {
            // TODO: Update this to implement email confirmation if required
            return Task.FromResult<object>(null);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
        }

        #region IUserLockoutStore<User, int>

        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            // TODO: Implement if failed count is required
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            // TODO: Implement if failed count is required
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            // TODO: Implement if failed count is required
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            // TODO: Implement if failed count is required
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            // TODO: Implement if failed count is required
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            // TODO: Implement if lockout is required
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            // TODO: Implement if failed count is required
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore<User, int>

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return _context.SaveChangesAsync();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion

        #region IUserTwoFactorStore<User, int>

        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            // TODO: Implement if two factor authentication is required
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            // TODO: Implement if two factor authentication is required
            return Task.FromResult(false);
        }

        #endregion

        #region IUserClaimStore<User, int>

        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            IList<Claim> _claims = new List<Claim>
            {
                new Claim("ID", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            return Task.FromResult(_claims);
        }

        public Task AddClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId)
        {
            return _context.Users.FindAsync(userId);
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
