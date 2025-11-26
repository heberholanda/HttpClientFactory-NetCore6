using Domain;
using Refit;

namespace Generated_Clients.Interfaces
{
    /// <summary>
    /// Refit interface for CRUD operations with JSONPlaceholder API
    /// Each method is mapped to a specific HTTP operation through attributes
    /// Refit generates all implementation automatically
    /// </summary>
    public interface IJsonPlaceholderClient
    {
        /// <summary>
        /// Fetches all posts
        /// </summary>
        [Get("/posts")]
        Task<IEnumerable<Post>> GetAllAsync();

        /// <summary>
        /// Creates a new post
        /// The post parameter is automatically serialized to JSON in the request body
        /// </summary>
        [Post("/posts")]
        Task<Post> Post(Post post);

        /// <summary>
        /// Updates an existing post
        /// The {id} in the route will be replaced with the id parameter value
        /// </summary>
        [Put("/posts/{id}")]
        Task<Post> Put(int id, Post post);

        /// <summary>
        /// Removes a post
        /// The {id} in the route will be replaced with the id parameter value
        /// </summary>
        [Delete("/posts/{id}")]
        Task Delete(int id);
    }
}
