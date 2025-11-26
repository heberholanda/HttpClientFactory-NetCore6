using System;
using System.Net;

namespace Typed_clients.Middlewares
{
    /// <summary>
    /// Custom handler that intercepts HTTP requests
    /// Demonstrates how to add validation or custom logic before sending requests
    /// This handler is registered as AddHttpMessageHandler in Program.cs
    /// </summary>
    public class ValidateHeaderHandler : DelegatingHandler
    {
        /// <summary>
        /// Method that intercepts all HTTP requests
        /// Validates if the UserAgent header contains "HttpRequestsSample"
        /// If it doesn't contain it, returns BadRequest without sending the request to the server
        /// </summary>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Validates if UserAgent contains the expected value
            if (!request.Headers.UserAgent.ToString().Contains("HttpRequestsSample"))
            {
                // Returns BadRequest without making the request to the external API
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("In this case I'm validating if the UserAgent contains a certain value.")
                };
            }

            // If validation passes, continues with the normal request
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
