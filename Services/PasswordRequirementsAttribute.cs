using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication71.Services
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PasswordRequirementsAttribute : ValidationAttribute
    {
        public PasswordRequirementsAttribute () : base("Hasło musi zawierać co najmniej jedną małą literę, jedną dużą literę, jedną cyfrę i jeden znak specjalny.")
        {
        }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var password = value.ToString();

                if (!ContainsLowercase(password))
                {
                    return new ValidationResult("Hasło musi zawierać co najmniej jedną małą literę.");
                }

                if (!ContainsUppercase(password))
                {
                    return new ValidationResult("Hasło musi zawierać co najmniej jedną dużą literę.");
                }

                if (!ContainsDigit(password))
                {
                    return new ValidationResult("Hasło musi zawierać co najmniej jedną cyfrę.");
                }

                if (!ContainsSpecialCharacter(password))
                {
                    return new ValidationResult("Hasło musi zawierać co najmniej jeden znak specjalny.");
                }
            }

            return ValidationResult.Success;
        }

        private bool ContainsLowercase (string password)
        {
            return password.Any(char.IsLower);
        }

        private bool ContainsUppercase (string password)
        {
            return password.Any(char.IsUpper);
        }

        private bool ContainsDigit (string password)
        {
            return password.Any(char.IsDigit);
        }

        private bool ContainsSpecialCharacter (string password)
        {
            return password.Any(ch => !char.IsLetterOrDigit(ch));
        }
    }
}
