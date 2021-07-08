using System;
using System.Net;
using System.Net.Http;

namespace JeweleryShopApi.Helpers
{
    public class ResponseException : Exception
    {
        public HttpStatusCode Code { get; set; }
        public string ErrorInformation { get; set; }

        public ResponseException(HttpStatusCode code, string errorInformation)
        {
            this.Code = code;
            this.ErrorInformation = errorInformation;
        }
    }
}