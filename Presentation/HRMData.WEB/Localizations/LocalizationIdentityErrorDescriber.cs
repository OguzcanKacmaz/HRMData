using Microsoft.AspNetCore.Identity;

namespace HRMData.WEB.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new() { Code = "DuplicateUserName", Description = $"{userName} daha önce başka bir kullanıcı tarafından alınmıştır" };
            //return base.DuplicateUserName(userName);
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new() { Code = "DuplicateEmail", Description = $"{email} daha önce başka bir kullanıcı tarafından alınmıştır" };
            //return base.DuplicateEmail(email);
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new() { Code = "PasswordTooShort", Description = $"Şifre En Az {length} karakterli olmalıdır" };
            //return base.PasswordTooShort(length);
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code = "PasswordRequiresNonAlphanumeric", Description = $"Şifre En Az 1 Alphanumeric Karakter İçermelidir" };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new() { Code = "PasswordRequiresDigit", Description = $"Şifre En Az Bir Rakamdan ('0'-'9') Oluşmalıdır" };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new() { Code = "PasswordRequiresUpper", Description = $"Şifrede En Az Bir Büyük Harf ('A'-'Z') Bulunmalıdır" };
        }
        public override IdentityError InvalidToken()
        {
            return new() { Code = "PasswordRequiresUpper", Description = $"Şifre Sıfırlama Bağlantısının Süresi Doldu.Lütfen Yeni Talepde Bulunun" };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new() { Code = "PasswordRequiresLower", Description = $"Şifrede En Az Bir Küçük Harf ('a'-'z') Bulunmalıdır." };
        }
    }
}
