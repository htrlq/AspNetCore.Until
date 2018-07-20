using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AspNetCore.Until.Test
{
    public class TokenTest
    {
        [Fact]
        public async void TokenControllerTest()
        {
            string url = "http://localhost:5000/api/Token";
            string json = "{\n" +
                          "\"PcName\":\"HtrPc\",\n" +
                          "\"UserName\":\"HtrUser\",\n" +
                          "\"Version\":\"Windows10\"\n" +
                          "}";
            var ContentType = "application/json";
            var failtoken = "4EE3AD52726D8FD150E88D83AE43263C";
            var token = "4EE3AD52726D8FD150E88D83AE43263A";

            var result = await url.HttpPostAsync(json, null, ContentType, 3000, Encoding.UTF8);
            Assert.Equal("Not Token", result);

            var headers = new Dictionary<string, string>();
            headers.Add("Token", failtoken);

            var failResult = await url.HttpPostAsync(json,
                headers
                , ContentType, 3000, Encoding.UTF8);

            Assert.Equal("Check Token Not Success", failResult);

            headers["Token"] = token;
            var successResult = await url.HttpPostAsync(json,
                headers
                , ContentType, 3000, Encoding.UTF8);

            Assert.NotEqual("Check Token Not Success", successResult);
        }

        [Fact]
        public async void TokenAttributeTest()
        {
            string url = "http://localhost:5000/api/Api";
            string json = "{\n" +
                          "\"PcName\":\"HtrPc\",\n" +
                          "\"UserName\":\"HtrUser\",\n" +
                          "\"Version\":\"Windows10\"\n" +
                          "}";
            var ContentType = "application/json";
            var failtoken = "4EE3AD52726D8FD150E88D83AE43263C";
            var token = "4EE3AD52726D8FD150E88D83AE43263A";

            var result = await url.HttpPostAsync(json, null, ContentType, 3000, Encoding.UTF8);
            Assert.Equal("Not Token", result);

            var headers = new Dictionary<string, string>();
            headers.Add("Token", failtoken);

            var failResult = await url.HttpPostAsync(json,
                headers
                , ContentType, 3000, Encoding.UTF8);

            Assert.Equal("Check Token Not Success", failResult);

            headers["Token"] = token;
            var successResult = await url.HttpPostAsync(json,
                headers
                , ContentType, 3000, Encoding.UTF8);

            Assert.NotEqual("Check Token Not Success", successResult);
        }
    }
}
