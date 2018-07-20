using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCore.Until
{
    public sealed class TokenCheck
     {
        private static string CreateMD5Hash(string input)
        {

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static void Checked(ActionExecutingContext context)
        {
            var Arguments = context.ActionArguments;
            var httpContext = context.HttpContext;
            var request = httpContext.Request;

            var token = "Token";
            List<object> objList = new List<object>();
            if (request.Headers.Keys.Contains(token))
            {
                var tokenValue = request.Headers[token].ToString();

                StringBuilder builder = new StringBuilder();

                foreach (var item in Arguments.Keys)
                {
                    var obj = Arguments[item];
                    var objType = obj.GetType();

                    if (obj != null)
                    {
                        objList.Add(obj);

                        foreach (var propertyInfo in objType.GetProperties())
                        {
                            var value = propertyInfo.GetValue(obj);
                            if (value != null)
                                builder.Append(value.ToString());
                        }
                    }
                }

                var values = builder.ToString();
                var md5 = CreateMD5Hash(values);

                ITokenCheck tokenCheck = null;

                try
                {
                    tokenCheck = httpContext.RequestServices.GetService(typeof(ITokenCheck)) as ITokenCheck;
                }
                catch
                {

                }

                if (!tokenValue.ToUpper().Equals(md5))
                {
                    foreach (var obj in objList)
                        tokenCheck?.Valid(obj);
                    throw new Exception($"Check Token Not Success");
                }
            }
            else
            {
                throw new Exception("Not Token");
            }
        }
     }
}
