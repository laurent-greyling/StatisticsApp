using StatisticsApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace StatisticsApp.Helpers
{
    public class RestApi
    {
        public WebRequest Get(string url, AccessToken token)
        {
            var request = WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Timeout = 15000;
            request.Headers.Add("Authorization", $"Basic {token.AuthenticationToken}");
            return request;
        }

        public WebRequest Post(string url)
        {
            var request = WebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = 15000;

            return request;
        }
    }
}
