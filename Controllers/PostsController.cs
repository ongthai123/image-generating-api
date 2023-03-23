using Image_Generating_APIs.Interfaces;
using Image_Generating_APIs.Models;
using Image_Generating_APIs.Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Image_Generating_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMongoDBService _mongoDBService;
        private string APIKey = string.Empty;

        public PostsController(IMongoDBService mongoDBService, IConfiguration configuration)
        {
            _mongoDBService = mongoDBService;
            APIKey = configuration.GetSection("OpenAiApiKey").Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mongoDBService.GetPostsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            using var client = new HttpClient();

            var input = new Input
            {
                prompt = post.Prompt,
                n = 1,
                size = "1024x1024"
            };

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", APIKey);

            var response = await client.PostAsync("https://api.openai.com/v1/images/generations", new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json"));

            var stringResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ResponseModel>(stringResponse);

                var postToSave = new Post 
                { 
                    Id = Guid.NewGuid().ToString(), 
                    Name = post.Name, 
                    Prompt = post.Prompt, 
                    PhotoUrl = result?.Data[0]?.Url 
                };

                await _mongoDBService.CreatePostAsync(postToSave);

                return Ok(postToSave);
            }

            return BadRequest();
        }
    }
}
