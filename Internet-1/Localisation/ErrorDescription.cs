using Microsoft.AspNetCore.Identity;

namespace Internet_1.Localisation
{
    public class ErrorDescription : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DuplicateUserName", Description = $"{userName}, kullanıcı adı kayıtlıdır!" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"{email}, E-Posta adresi kayıtlıdır!" };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code = "PasswordRequiresNonAlphanumeric", Description = "Parola en az bir alfanumerik olmayan karakter içermelidir!" };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new() { Code = "PasswordRequiresDigit", Description = "Parola en az bir rakamdan (0-9) oluşmalıdır!" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Parola en az {length} karakter olmalıdır!" };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new() { Code = "PasswordRequiresLower", Description = $"Parolada en az bir küçük harf ('a'-'z') olmalıdır!" };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new() { Code = "PasswordRequiresUpper", Description = $"Parolada en az bir büyük harf ('A'-'Z') olmalıdır!" };
        }

    }
}
