using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NextCMS.Core
{
    /// <summary>
    /// 加密 & 解密
    /// </summary>
    public class Encryption
    {
        private readonly static string _encryptionKey = "ABCDEFGHIJKL123456789";

        /// <summary>
        /// 盐值
        /// </summary>
        /// <param name="size">长度</param>
        /// <returns>盐值</returns>
        public static string CreateSaltKey(int size)
        {
            //创建随机数
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            //将随机数转换为Base64并且返回
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 哈希密码
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="saltkey">盐值</param>
        /// <param name="passwordFormat">密码格式 (hash algorithm)</param>
        /// <returns>哈希密码</returns>
        public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);

            //return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name", "hashName");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">需要加密的字符串</param>
        /// <param name="encryptionPrivateKey">私有key值</param>
        /// <returns>加密字符串</returns>
        public static string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _encryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText">需要解密的字符串</param>
        /// <param name="encryptionPrivateKey">私有key值</param>
        /// <returns>解密字符串</returns>
        public static string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            if (String.IsNullOrEmpty(encryptionPrivateKey))
                encryptionPrivateKey = _encryptionKey;

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] buffer = Convert.FromBase64String(cipherText);
            return DecryptTextFromMemory(buffer, tDESalg.Key, tDESalg.IV);
        }

        #region 公用

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private static string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    var sr = new StreamReader(cs, new UnicodeEncoding());
                    return sr.ReadLine();
                }
            }
        }

        #endregion
    }
}
