using DigitalWallet.Domain.Common;
using System;
using System.Collections.Generic;

namespace DigitalWallet.Domain.ValueObjects
{
    public class Fullname : ValueObject
    {
        #region Ctor

        public Fullname()
        {

        }
        public Fullname(string name, string surname)
        {
            if (name.Length > 50)
            {
                throw new ArgumentOutOfRangeException("name can not be more than 50 characters");
            }
            else
            {
                Name = name;
            }

            if (surname.Length > 50)
            {
                throw new ArgumentOutOfRangeException("surname can not be more than 50 characters");
            }
            else
            {
                Surname = surname;
            }
        }

        #endregion

        public string? Name { get; private set; } = null;
        public string? Surname { get; private set; } = null;

        public string? GetName() => Name;
        public string? GetSurname() => Surname;
        public string GetFullName() => $"{Name} {Surname}";
        public void SetFullName(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        #region Overrides

        public static bool operator ==(Fullname left, Fullname right)
        {
            if (left is null || right is null)
            {
                if (left is null)
                {
                    return right is null;
                }

                return left.Equals(right);
            }

            return $"{left.Name}{left.Surname}" == $"{right.Name}{right.Surname}";
        }

        public static bool operator !=(Fullname left, Fullname right) => $"{left.Name}{left.Surname}" != $"{right.Name}{right.Surname}";

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Surname;
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

            return ((Fullname)obj == this);
        }

        public override int GetHashCode() => this!.GetHashCode();

        #endregion
    }
}
