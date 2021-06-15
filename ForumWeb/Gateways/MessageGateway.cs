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
    public class MessageGateway
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public MessageGateway(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<List<Message>> GetMessage()
        {
            /*var response = await _httpClient.GetAsync(_configuration["ForumAPI"] + "/Comments");
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Comment>>(apiResponse);*/
            return await _httpClient.GetFromJsonAsync<List<Message>>(_configuration["ForumAPI"] + "/Messages");
        }

        public async Task PostMessage(Message message)
        {
            await _httpClient.PostAsJsonAsync(_configuration["ForumAPI"] + "/Messages", message);
        }

        public async Task<Message> DeleteMessage(Guid deleteId)
        {
            var response = await _httpClient.DeleteAsync(_configuration["ForumAPI"] + "/Messages/" + deleteId);
            Message returnValue = await response.Content.ReadFromJsonAsync<Message>();

            return returnValue;
        }

        public async Task PutMessage(Guid editId, Message message)
        {
            await _httpClient.PutAsJsonAsync(_configuration["ForumAPI"] + "/Messages/" + editId, message);
        }
    }
}
