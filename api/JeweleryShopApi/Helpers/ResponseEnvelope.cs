using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JeweleryShopApi.Helpers
{
    public class ResponseEnvelope : IActionResult, ISerializable
    {
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ResponseEnvelope(HttpStatusCode code, string message, object data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(this)
            {
                StatusCode = (int)this.Code
            };

            await objectResult.ExecuteResultAsync(context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}