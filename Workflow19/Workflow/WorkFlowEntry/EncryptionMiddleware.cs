using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WorkFlowEntry
{
    //For encrypt decrypt string https://demo.kenhaggerty.com/Demos/AesCipher
    //Refered example https://tudip.com/blog-post/how-to-securely-transfer-web-api-data-in-asp-net-core/


    public class EncryptionMiddleware
    {
        private readonly RequestDelegate _next;
        public EncryptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Body = EncryptStream(httpContext.Response.Body);
            httpContext.Request.Body = DecryptStream(httpContext.Request.Body);
            //if (httpContext.Request.QueryString.HasValue)
            //{
            //    string decryptedString = DecryptString(httpContext.Request.QueryString.Value.Substring(1));
            //    httpContext.Request.QueryString = new QueryString($"?{decryptedString}");
            //}
            await _next(httpContext);
            await httpContext.Request.Body.DisposeAsync();
            await httpContext.Response.Body.DisposeAsync();
        }

        private CryptoStream EncryptStream(Stream responseStream)
        {
            Aes aes = GetEncryptionAlgorithm();

            ToBase64Transform base64Transform = new ToBase64Transform();
            CryptoStream base64EncodedStream = new CryptoStream(responseStream, base64Transform, CryptoStreamMode.Write);
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(base64EncodedStream, encryptor, CryptoStreamMode.Write);

            return cryptoStream;
        }

        private Stream DecryptStream(Stream cipherStream)
        {
            Aes aes = GetEncryptionAlgorithm();

            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
            CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);

            return decryptedStream;
        }

        private Aes GetEncryptionAlgorithm()
        {
            Aes aes = Aes.Create();
            aes.Key = Convert.FromBase64String("3g6/EbHi2XEsQN2SLUSsGzve4egTo4+lnU+qScCXLSY=");
            aes.IV = Convert.FromBase64String("dv2TIgNjQPWjydZyynZvHA==");

            return aes;
        }
    }

}

