using System;
using AppliedSystems.Interfaces;
using Microsoft.AspNet.Identity;
using Moq;

namespace AppliedSystems.Tests.Common
{
    public sealed class FakePasswordHasher
    {
        private readonly Mock<IAppliedSystemsPasswordHasher> _passwordHasher;
        private string _salt;
        private PasswordVerificationResult? _result;
        
        public FakePasswordHasher()
        {
            _salt = string.Empty;
            _passwordHasher = new Mock<IAppliedSystemsPasswordHasher>();
            _result = null;
        }

        public FakePasswordHasher WithPasswordSalt(string salt)
        {
            _salt = salt;
            return this;
        }

        public FakePasswordHasher WithResult(PasswordVerificationResult result)
        {
            _result = result;
            return this;
        }

        public Mock<IAppliedSystemsPasswordHasher> Build()
        {
            _passwordHasher
                .Setup(s => s.HashPassword(It.IsAny<string>()))
                .Returns((string passwordHash) => passwordHash);

            _passwordHasher
                .SetupGet(s => s.Salt)
                .Returns(_salt);

            if (_result.HasValue)
            {
                _passwordHasher
                    .Setup(ph => ph.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(_result.Value);

                _passwordHasher
                    .Setup(ph => ph.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(_result.Value == PasswordVerificationResult.Success);
            }
            else
            {
                _passwordHasher
                    .Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(
                        (string hash, string pass) =>
                            hash.Equals(pass, StringComparison.InvariantCultureIgnoreCase)
                                ? PasswordVerificationResult.Success
                                : PasswordVerificationResult.Failed);

                _passwordHasher
                    .Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns((string pass, string salt, string hash) => hash.Equals(pass, StringComparison.InvariantCultureIgnoreCase));
            }

            return _passwordHasher;
        }
    }
}
