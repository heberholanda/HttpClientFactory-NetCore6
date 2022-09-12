using Domain;
using Refit;

namespace Generated_Clients.Interfaces
{
    public interface IJsonPlaceholderClient
    {
        [Get("/posts")]
        Task<IEnumerable<Post>> GetAllAsync();

        [Post("/posts")]
        Task<Post> Post(Post post);

        [Put("/posts/{id}")]
        Task<Post> Put(int id, Post post);

        [Delete("/posts/{id}")]
        Task Delete(int id);
    }
}
