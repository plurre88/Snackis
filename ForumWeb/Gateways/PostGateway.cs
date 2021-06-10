using ForumWeb.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ForumWeb.Gateways
{
    public class PostGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public PostGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Post>> GetPost()
        {
            /*var response = await _httpClient.GetAsync(_configuration["ForumAPI"] + "/Posts");
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Post>>(apiResponse);*/
            return await _httpClient.GetFromJsonAsync<List<Post>>(_configuration["ForumAPI"] + "/Posts");
        }

        public async Task<Post> PostPosts(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["ForumAPI"] + "/Posts", post);
            Post returnValue = await response.Content.ReadFromJsonAsync<Post>();

            return returnValue;
        }

        public async Task<Post> DeletePost(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["ForumAPI"] + "/Posts/" + deleteId);
            Post returnValue = await response.Content.ReadFromJsonAsync<Post>();

            return returnValue;
        }

        public async Task PutPost(Guid editId, Post post)
        {
            await _httpClient.PutAsJsonAsync(_configuration["ForumAPI"] + "/Posts/" + editId, post);
        }
    }
}
