using System.Security.Cryptography;
using System.Text;

namespace his.Helper
{
    public static class Helpers
    {
        public static string ComputeSha256Hash(string rawData)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }

        public static string RandomString(string prefix, int len = 6)
        {
            var rnd = new Random();
            var sb = new StringBuilder(prefix);
            for (int i = 0; i < len; i++)
                sb.Append(rnd.Next(0, 10));
            return sb.ToString();
        }

        public static string RandomDate(string format)
        {
            var rnd = new Random();
            var date = DateTime.Now.AddDays(-rnd.Next(365 * 80));
            return date.ToString(format);
        }

        public static string RandomGender()
        {
            return new Random().Next(0, 2) == 0 ? "Nam" : "Nữ";
        }

        public static string RandomFullName()
        {
            string[] ho = { "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Võ", "Đặng" };
            string[] tenDem = { "Văn", "Thị", "Hữu", "Minh", "Ngọc" };
            string[] ten = { "An", "Bình", "Châu", "Dũng", "Hạnh", "Lan", "Phúc", "Quân" };
            var rnd = new Random();
            return $"{ho[rnd.Next(ho.Length)]} {tenDem[rnd.Next(tenDem.Length)]} {ten[rnd.Next(ten.Length)]}";
        }

        public static string RandomBloodGroup()
        {
            string[] groups = { "A", "B", "AB", "O" };
            return groups[new Random().Next(groups.Length)];
        }

        public static int CalAge(DateTime birthdate)
        {
            var today = DateTime.Today;
            int age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age;
        }

        public static string Truncate(string value, int maxLength = 100)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
    }
}
