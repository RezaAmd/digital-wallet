using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace DigitalWallet.Domain.ValueObjects
{
    public class PasswordHash : ValueObject
    {
        public string? Value { get; private set; } = null;

        #region Ctor & Props

        // Minimum password character length.
        private static int _minimumLength = 4;
        // Maximum password character length.
        private static int _maximumLength = 50;

        public PasswordHash() { }
        public PasswordHash(string password)
        {
            _setValue(password);
        }

        #endregion

        #region Methods

        private string _encode(string password)
        {
            var saltBytes = Encoding.ASCII.GetBytes("6067CF0158F61D1751751D5FD4F7E287");
            var hashPasswordBytes =
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8);

            return string.Concat(
                Convert.ToBase64String(saltBytes),
                "-TOI-",
                Convert.ToBase64String(hashPasswordBytes));
        }
        private void _setValue(string password)
        {
            // Validation
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Password cannot be null or empty.");
            if (password.Length < _minimumLength)
                throw new ArgumentOutOfRangeException($"Password characters cannot be less than {_minimumLength}.");
            if (password.Length > _maximumLength)
                throw new ArgumentOutOfRangeException($"Password characters cannot be more than {_maximumLength}.");

            Value = _encode(password);
        }

        public static PasswordHash Parse(string password)
            => new PasswordHash(password);
        public override int GetHashCode() => Value!.GetHashCode();
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        #endregion

        #region Operators

        public static bool operator ==(PasswordHash left, PasswordHash right)
        {
            //https://stackoverflow.com/questions/4219261/overriding-operator-how-to-compare-to-null
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }
        public static bool operator !=(PasswordHash left, PasswordHash right)
        {
            if (left is null)
            {
                return right is not null;
            }

            return left.Value != right.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return ((PasswordHash)obj == this);
        }

        #endregion

        #region highe level encryption

        public bool VerifyPasswordHash(string password)
        {
            var splitHash = Value.Split("-TOI-");

            var saltHash = Convert.FromBase64String(splitHash[0]);
            var passwordHashed = Convert.FromBase64String(splitHash[1]);

            var newhashPasswordBytes =
                KeyDerivation.Pbkdf2(
                password: password,
                salt: saltHash,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);

            return passwordHashed.SequenceEqual(newhashPasswordBytes);
        }
        private static byte[] Generate128BitSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        #endregion
    }
}