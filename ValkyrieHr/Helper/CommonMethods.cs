using System.Text;

namespace ValkyrieHr.Helper
{
    public static class CommonMethods
    {
        public static string EncodeString(string value)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }
        public static string DecodeString(string value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
        // allow Images extension and size
        public static bool IsValidImageFile(IFormFile file)
        {
            string[] allExtension = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ext = Path.GetExtension(file.FileName);
            if (allExtension.Contains(ext) && file.Length < 2000000)
            {
                return true;
            }
            return false;
        }
    }
}

