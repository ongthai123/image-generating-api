using Image_Generating_APIs.Interfaces;
using Image_Generating_APIs.Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Image_Generating_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMongoDBService _mongoDBService;

        public PostsController(IMongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<List<Post>> Get() => await _mongoDBService.GetPostsAsync();

        [HttpPost]
        public async Task Create([FromBody] Post post)
        {
            await _mongoDBService.Create(post);
        }
    }
}
