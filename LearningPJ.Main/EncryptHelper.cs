using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LearningPJ.Main
{
    public class EncryptHelper
    {

        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public static string LoginPasswordKey = "qulei930";

        private static string LoginPassword = "uk6sUZRwyCP+IDK67PTX5A==";

        /// <summary>  

        /// DES加密字符串  

        /// </summary>  

        /// <param name="encryptString">待加密的字符串</param>  

        /// <param name="encryptKey">加密密钥,要求为8位</param>  

        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>  

        public static string EncryptDES(string encryptString, string encryptKey)
        {

            try
            {

                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));

                byte[] rgbIV = Keys;

                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();

                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);

                cStream.FlushFinalBlock();

                return Convert.ToBase64String(mStream.ToArray());

            }

            catch
            {

                return encryptString;

            }

        }


        /// <summary>  

        /// DES解密字符串  

        /// </summary>  

        /// <param name="decryptString">待解密的字符串</param>  

        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>  

        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>  

        public static string DecryptDES(string decryptString, string decryptKey)
        {

            try
            {

                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);

                byte[] rgbIV = Keys;

                byte[] inputByteArray = Convert.FromBase64String(decryptString);

                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();

                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);

                cStream.Write(inputByteArray, 0, inputByteArray.Length);

                cStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(mStream.ToArray());

            }
            catch
            {

                return decryptString;

            }
        }

        public static string GetAppSettingByKey(string key)
        {
            return ConfigurationSettings.AppSettings[key];
        }

        public static List<EncryptModel> GetAllAppSetting()
        {
            List<EncryptModel> list = new List<EncryptModel>();
            string tempSetting;
            string[] tempObj;
            foreach (var k in ConfigurationSettings.AppSettings.AllKeys)
            {

                tempSetting = ConfigurationSettings.AppSettings[k];


                tempObj = tempSetting.Split('|');

                EncryptModel m = new EncryptModel();
                m.ID = Convert.ToInt32(tempObj[0]);
                m.Account = tempObj[1];
                m.Key = tempObj[2];
                m.Password = DecryptDES(tempObj[3], m.Key);
                list.Add(m);

                tempObj = null;

            }
            return list;
        }

        public static bool CheckPassword(string password)
        {
            if (EncryptDES(password, LoginPasswordKey).Equals(LoginPassword))
                return true;

            return false;

        }



    }
}
