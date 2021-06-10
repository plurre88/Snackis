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
    public class CommentGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CommentGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Comment>> GetComments()
        {
            /*var response = await _httpClient.GetAsync(_configuration["ForumAPI"] + "/Comments");
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Comment>>(apiResponse);*/
            return await _httpClient.GetFromJsonAsync<List<Comment>>(_configuration["ForumAPI"] + "/Comments");
        }

        public async Task<Comment> PostComments(Comment comment)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["ForumAPI"] + "/Comments", comment);
            Comment returnValue = await response.Content.ReadFromJsonAsync<Comment>();

            return returnValue;
        }

        public async Task<Comment> DeleteComment(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["ForumAPI"] + "/Comments/" + deleteId);
            Comment returnValue = await response.Content.ReadFromJsonAsync<Comment>();

            return returnValue;
        }

        public async Task PutComment(Guid editId, Comment comment)
        {
            await _httpClient.PutAsJsonAsync(_configuration["ForumAPI"] + "/Comments/" + editId, comment);
        }
    }
}
