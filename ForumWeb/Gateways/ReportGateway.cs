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
    public class ReportGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public ReportGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<List<Report>> GetReport()
        {
            /*var response = await _httpClient.GetAsync(_configuration["ForumAPI"] + "/Comments");
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Comment>>(apiResponse);*/
            return await _httpClient.GetFromJsonAsync<List<Report>>(_configuration["ForumAPI"] + "/Reports");
        }

        public async Task<Report> PostReport(Report report)
        {
            var response = await _httpClient.PostAsJsonAsync(_configuration["ForumAPI"] + "/Reports", report);
            Report returnValue = await response.Content.ReadFromJsonAsync<Report>();

            return returnValue;
        }

        public async Task DeleteReport(Guid deleteId)
        {
            await _httpClient.DeleteAsync(_configuration["ForumAPI"] + "/Reports/" + deleteId);
        }

        public async Task PutReport(Guid editId, Report report)
        {
            await _httpClient.PutAsJsonAsync(_configuration["ForumAPI"] + "/Reports/" + editId, report);
        }
    }
}
