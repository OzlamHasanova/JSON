using JsonFile.Exceptions;
using JsonFile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JsonFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var response = GetAsync("https://jsonplaceholder.typicode.com/users").Result;
            foreach (var item in response)
            {
                Console.WriteLine(item.UserName);
            }
        }
        static async Task<List<User>> GetAsync(string path)
        {
            List<User> user= default;
            using(HttpClient client = new HttpClient())
            {
                var responseMessage= await client.GetAsync(path);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseStr=await responseMessage.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<List<User>>(responseStr);

                }
                else
                {
                    throw new NotFoundException("data not found");
                }
            }
            return user;



        }
        
    }
}