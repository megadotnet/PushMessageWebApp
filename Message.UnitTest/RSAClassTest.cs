using Message.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Message.UnitTest
{
    public class RSAClassTest
    {
        [Fact]
        public void RSAParameters()
        {

            var rsa = new RSACryptoServiceProvider();
            var privateParameters = rsa.ExportParameters(true);
            var publicParameters = rsa.ExportParameters(false);

            //Export private parameter XML representation of privateParameters
            //object created above
            Console.WriteLine(rsa.ToXmlString(true));

            //Export private parameter XML representation of publicParameters
            //object created above
            Console.WriteLine(rsa.ToXmlString(false));
        }

        [Fact]
        public void VerifyToken()
        {

            var token = "User1";
            var encryptedToken = RSAClass.Encrypt(token);
            var decryptedToken = RSAClass.Decrypt(encryptedToken);

            Console.WriteLine(encryptedToken);
            Assert.Equal(token, decryptedToken);

        }
    }
}
