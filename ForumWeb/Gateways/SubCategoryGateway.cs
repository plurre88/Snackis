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
    public class SubCategoryGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public SubCategoryGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<List<SubCategory>> GetSubCategories()
        {
            /*var response = await _httpClient.GetAsync(_configuration["ForumAPI"] + "/SubCategories");
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<SubCategory>>(apiResponse);*/
            return await _httpClient.GetFromJsonAsync<List<SubCategory>>(_configuration["ForumAPI"] + "/SubCategories");
        }

        public async Task<SubCategory> PostSubCategories(SubCategory subCategory)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["ForumAPI"] + "/SubCategories", subCategory);
            SubCategory returnValue = await response.Content.ReadFromJsonAsync<SubCategory>();

            return returnValue;
        }

        public async Task<SubCategory> DeleteSubCategory(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["ForumAPI"] + "/SubCategories/" + deleteId);
            SubCategory returnValue = await response.Content.ReadFromJsonAsync<SubCategory>();

            return returnValue;
        }

        public async Task PutSubCategory(Guid editId, SubCategory subCategory)
        {
            await _httpClient.PutAsJsonAsync(_configuration["ForumAPI"] + "/SubCategories/" + editId, subCategory);
        }
    }
}
