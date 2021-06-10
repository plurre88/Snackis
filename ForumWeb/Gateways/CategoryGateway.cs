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
    public class CategoryGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CategoryGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<Category>> GetCategories()
        {
            /*var response = await _httpClient.GetAsync(_configuration["ForumAPI"] + "/Categories");
            var apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Category>>(apiResponse);*/
            return await _httpClient.GetFromJsonAsync<List<Category>>(_configuration["ForumAPI"] + "/Categories");
        }

        public async Task<Category> PostCategories(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["ForumAPI"] + "/Categories", category);
            Category returnValue = await response.Content.ReadFromJsonAsync<Category>();

            return returnValue;
        }

        public async Task DeleteCategory(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["ForumAPI"] + "/Categories/" + deleteId);
            /*Category returnValue = await response.Content.ReadFromJsonAsync<Category>();

            return returnValue;*/
        }

        public async Task PutCategory(Guid editId, Category category)
        {
            await _httpClient.PutAsJsonAsync(_configuration["ForumAPI"] + "/Categories/" + editId, category);
        }
    }
}
