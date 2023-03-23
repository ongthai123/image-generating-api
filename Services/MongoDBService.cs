using Image_Generating_APIs.Interfaces;
using Image_Generating_APIs.Models;
using Image_Generating_APIs.Models.Post;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Image_Generating_APIs.Services
{
    public class MongoDBService: IMongoDBService
    {
        private readonly IMongoCollection<Post> _postCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var client = new MongoClient(mongoDBSettings.Value.ConnectionURL);

            var database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);

            _postCollection = database.GetCollection<Post>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Post>> GetPostsAsync() => await _postCollection.Find(x => true).ToListAsync();

        public async Task CreatePostAsync(Post post)
        {
            await _postCollection.InsertOneAsync(post);
        }
    }
}
