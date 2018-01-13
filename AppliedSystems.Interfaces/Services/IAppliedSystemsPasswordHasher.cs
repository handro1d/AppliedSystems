using Microsoft.AspNet.Identity;

namespace AppliedSystems.Interfaces
{
    public interface IAppliedSystemsPasswordHasher : IPasswordHasher
    {
        string Salt { get; set; }

        bool VerifyHashedPassword(string password, string salt, string hash);

        string GenerateSecret(string password, out string salt);
    }
}
