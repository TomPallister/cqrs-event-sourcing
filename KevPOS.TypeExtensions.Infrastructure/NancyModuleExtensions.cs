using KevPOS.ValueObjects;
using Nancy;
using Nancy.Responses;

namespace KevPOS.TypeExtensions.Infrastructure
{
    public static class NancyModuleExtensions
    {
        public static JsonResponse<ResponseContent> GetResponse(ResponseContent responseContent)
        {
            if (responseContent != null)
            {
                return new JsonResponse<ResponseContent>(responseContent, new DefaultJsonSerializer())
                {
                    ContentType = "application/json",
                    StatusCode = HttpStatusCode.Accepted
                };
            }

            return new JsonResponse<ResponseContent>(null, new DefaultJsonSerializer())
            {
                ContentType = "application/json",
                StatusCode = HttpStatusCode.BadRequest
            };
        }
    }
}