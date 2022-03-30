using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using XamShell.Application.Models.DTOs;

namespace XamShell.Infrastructure.ExternalServices
{
    public class RestService:IRestService
    {
        HttpClient client;
        JsonSerializerOptions serializerOptions;

        public UserDto Items { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<UserDto> RefreshDataAsync()
        {
            Items = new UserDto();

            Uri uri = new Uri(string.Format("https://api.agify.io/?name=michael", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<UserDto>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}