using System;
using System.Threading.Tasks;
using AppliedSystems.Domain.DAO;
using AppliedSystems.Interfaces;
using Microsoft.AspNet.Identity;

namespace AppliedSystems.Services
{
    public class AppliedSystemsUserManager : UserManager<User, int>
    {
        private readonly IAppliedSystemsPasswordHasher _passwordHasher;

        public AppliedSystemsUserManager(IUserStore<User, int> store, IAppliedSystemsPasswordHasher passwordHasher) 
            : base(store)
        {
            if (passwordHasher == null)
            {
                throw new ArgumentNullException("passwordHasher");
            }

            _passwordHasher = passwordHasher;
        }

        public override async Task<User> FindAsync(string userName, string password)
        {
            var user = await FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            _passwordHasher.Salt = user.PasswordSalt;
            var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

            return result == PasswordVerificationResult.Success ? user : null;
        }

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user.PasswordHash);
            user.PasswordSalt = _passwordHasher.Salt;

            return await base.CreateAsync(user);
        }

        public override async Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.PasswordHash = _passwordHasher.HashPassword(password);
            user.PasswordSalt = _passwordHasher.Salt;

            return await base.CreateAsync(user);
        }

        protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<User, int> store, User user, string password)
        {
            _passwordHasher.Salt = user.PasswordSalt;
            var result = _passwordHasher.VerifyHashedPassword(user.PasswordHash, password);

            return Task.FromResult(result == PasswordVerificationResult.Success);
        }
    }
}
