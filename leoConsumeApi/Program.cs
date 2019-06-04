using System;
using System.Net.Http;

namespace leoConsumeApi
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
           string url = "http://localhost:5000/api/values";
           HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,url);
           HttpResponseMessage response = client.SendAsync(request).GetAwaiter().GetResult();
           Console.WriteLine($"output: {response.Content.ReadAsStringAsync().GetAwaiter().GetResult()}");
           Console.ReadLine();
        }
    }
}
