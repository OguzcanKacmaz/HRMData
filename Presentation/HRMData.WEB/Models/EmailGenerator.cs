namespace HRMData.WEB.Areas.HRMPanel.Models
{
    public class EmailGenerator
    {
        public static string GenerateEmail(string firstName, string lastName, string middleName = "")
        {
            // Türkçe karakterleri İngilizce karakterlere dönüştürmek için TurkishToEnglish metodu kullanılır
            firstName = TurkishToEnglish(firstName);
            lastName = TurkishToEnglish(lastName);
            if (middleName != null)
                middleName = TurkishToEnglish(middleName);


            // Mail adresi oluşturulur
            string email = "";
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                if (!string.IsNullOrWhiteSpace(middleName))
                {
                    email = $"{firstName}{middleName}.{lastName}@bilgeadamboost.com";
                }
                else
                {
                    email = $"{firstName}.{lastName}@bilgeadamboost.com";
                }
            }

            return email;
        }

        // Türkçe karakterleri İngilizce karakterlere dönüştüren metot
        public static string TurkishToEnglish(string str)
        {
            string[] trMap = new string[] { "ç", "Ç", "ğ", "Ğ", "ı", "İ", "ö", "Ö", "ş", "Ş", "ü", "Ü" };
            string[] engMap = new string[] { "c", "C", "g", "G", "i", "I", "o", "O", "s", "S", "u", "U" };

            for (int i = 0; i < trMap.Length; i++)
            {
                str = str.Replace(trMap[i], engMap[i]);
            }

            return str.ToLower();
        }
    }
}
