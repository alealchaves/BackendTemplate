using System;
using System.Security.Cryptography;
using System.Text;

namespace BackendTemplate.Infra.CrossCode
{
    public static class StringExtensions
    {
        public static string ToSHA(this string str)
        {
            var encoding = Encoding.GetEncoding(0);

            byte[] buffer = encoding.GetBytes(str);
            var sha1 = SHA1.Create();
            var hash = BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "").ToLower();

            return hash;
        }
    }
}
