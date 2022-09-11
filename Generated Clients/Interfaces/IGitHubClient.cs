using Domain;
using Refit;

namespace Generated_Clients.Interfaces
{
    public interface IGitHubClient
    {
        [Get("/repos/dotnet/AspNetCore.Docs/branches")]
        Task<IEnumerable<GitHubBranch>> GetAspNetCoreDocsBranchesAsync();
    }
}
