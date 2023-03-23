using Image_Generating_APIs.Models.Post;

namespace Image_Generating_APIs.Interfaces
{
    public interface IMongoDBService
    {
        Task<List<Post>> GetPostsAsync();
        Task CreatePostAsync(Post post);
    }
}
