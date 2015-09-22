using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Message.WebAPI.Services
{

    /// <summary>
    ///RSAClass
    /// </summary>
    /// <see cref="http://codebetter.com/johnvpetersen/2012/04/02/making-your-asp-net-web-apis-secure/"/>
    public class RSAClass
    {
        private static string _privateKey = "<RSAKeyValue><Modulus>xiGkd3ptspY8n8POOmIU3+fkBAceDEPy2z6HDmBcQbAkewXAnaGK+0nO5txa1CaZKL9xz/6vEMcRu36R7O3mRf02Tmx7+xno9cmrKHhiZPfweO0FL3cq9T62/hbIxLm3iyAMYRQsNoDZMPE1tLCSXr5XIVRrb75UqrIcfGss9m8=</Modulus><Exponent>AQAB</Exponent><P>445OF9b6usTdXCf6L9ODmlF7hTBkgBJpvgbO9aA8LlB/eKaZOorBsz9HuYnwFvFFwX9+Af+mxr1yr35wpCXiXQ==</P><Q>3uXEQT64+A7HbaVMIlIPdjRMEBnZZVt01+xp0M71JRL/68gsAIkd2NwObR60ghyYoisZEcHLXWEBBwIgcLJHOw==</Q><DP>p9PZls2QKFVvaTt20vUtt4/nCMkzJh3ubR86Xn/qQsJN8V713e5eg+Pk81tffpw9tUNhXPn/N86bmgEn9HiYZQ==</DP><DQ>iwCy4RVieJ+O0tvwnL6cEdUPUkMshb8BIN64JpXJ3zL4EmwktLjNuj2RaY9qdnGq5gcGfUJjtnoWD+7NmDo1nQ==</DQ><InverseQ>xmFPKpCtLEKVfmpkO35GrnIi6SjEr4aZAPsrkxpqx1Qyx5prrqO1tO4iyd8kkCpRUHdShl7/RrFS+2Y5w+ZP2Q==</InverseQ><D>W7oVj27xvF1Lodef8W2ZJnQQH0FASwNmOtR+6Ev6SjsHGKF4JEI2utHX5Q+dSVy5SaEgSuApIoZXEkutuVgGZA4kAHYmwuXZe4+JpPp46+DMh2ALX21OcmAdiosBfXlBbdElTZK7bv6Nqu5dUlbg9B+Gg9ceHBe6A8iGzfRA/Dk=</D></RSAKeyValue>";
        private static string _publicKey = "<RSAKeyValue><Modulus>xiGkd3ptspY8n8POOmIU3+fkBAceDEPy2z6HDmBcQbAkewXAnaGK+0nO5txa1CaZKL9xz/6vEMcRu36R7O3mRf02Tmx7+xno9cmrKHhiZPfweO0FL3cq9T62/hbIxLm3iyAMYRQsNoDZMPE1tLCSXr5XIVRrb75UqrIcfGss9m8=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });
            byte[] dataByte = new byte[dataArray.Length];
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(_privateKey);
            var decryptedByte = rsa.Decrypt(dataByte, false);
            return _encoder.GetString(decryptedByte);
        }

        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);
            var dataToEncrypt = _encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sb = new StringBuilder();
            foreach (var x in encryptedByteArray)
            {
                item++;
                sb.Append(x);

                if (item < length)
                    sb.Append(",");
            }

            return sb.ToString();
        }
    }



    public class AuthorizedUserRepository
    {
        public static IQueryable<User> GetUsers()
        {

            IList<User> users = new List<User>();
            users.Add(new User("User1"));
            users.Add(new User("User2"));
            users.Add(new User("User3"));
            users.Add(new User("Administrator"));

            return users.AsQueryable();
        }
    }
}