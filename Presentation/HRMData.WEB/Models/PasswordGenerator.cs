namespace HRMData.WEB.Areas.HRMPanel.Models
{
    public static class PasswordGenerator
    {
        private static readonly Random random = new Random();
        private const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
        private const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string digits = "0123456789";
        private const string alfanumerik = "*!@_?";

        public static string GeneratePassword(int length = 10)
        {
            var password = new List<char>();

            // Şifre oluşturma döngüsü
            while (true)
            {
                // Rastgele bir şifre oluşturma
                password = Enumerable.Repeat(0, length)
                    .Select(_ => GetRandomChar())
                    .ToList();

                // Şifrenin gereksinimleri karşılamadığı durumda döngüyü devam ettirme
                if (IsValidPassword(password))
                {
                    break;
                }
            }

            // Şifreyi dizeye dönüştürme ve geri döndürme
            return new string(password.ToArray());
        }

        private static char GetRandomChar()
        {
            // Rastgele bir karakter seçme
            string chars = lowerChars + upperChars + digits + alfanumerik;
            int index = random.Next(chars.Length);
            return chars[index];
        }

        private static bool IsValidPassword(List<char> password)
        {
            // Şifre en az 8 karakter uzunluğunda olmalıdır
            if (password.Count < 8)
                return false;

            // Şifre en az 1 küçük harf, 1 büyük harf, 1 sayı ve 1 alfanumerik karakter içermelidir
            bool hasLower = false;
            bool hasUpper = false;
            bool hasDigit = false;
            bool hasAlphanumeric = false;

            foreach (char c in password)
            {
                if (lowerChars.Contains(c))
                    hasLower = true;

                if (upperChars.Contains(c))
                    hasUpper = true;

                if (digits.Contains(c))
                    hasDigit = true;

                if (alfanumerik.Contains(c))
                    hasAlphanumeric = true;
            }

            return hasLower && hasUpper && hasDigit && hasAlphanumeric;
        }
    }
}
