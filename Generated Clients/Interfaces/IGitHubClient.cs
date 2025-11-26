using Domain;
using Refit;

namespace Generated_Clients.Interfaces
{
    /// <summary>
    /// Refit interface for communication with GitHub API
    /// Refit automatically generates the implementation of this interface at runtime
    /// No need to write code to make HTTP requests
    /// </summary>
    public interface IGitHubClient
    {
        /// <summary>
        /// Fetches branches from AspNetCore.Docs repository
        /// The [Get] attribute indicates it's a GET request
        /// Refit automatically makes the request and deserializes the JSON
        /// </summary>
        [Get("/repos/dotnet/AspNetCore.Docs/branches")]
        Task<IEnumerable<GitHubBranch>> GetAspNetCoreDocsBranchesAsync();
    }
}
