using System;
using System.Net;

namespace Typed_clients.Middlewares
{
    public class ValidateHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!request.Headers.UserAgent.ToString().Contains("HttpRequestsSample"))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("In this case I'm validating if the UserAgent contains a certain value.")
                };
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
