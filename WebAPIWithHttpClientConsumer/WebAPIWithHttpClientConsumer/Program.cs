using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
//using System.Net.Http.Formatting;
namespace WebAPIWithHttpClientConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:7967/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));            
            MyAPIGet(cons).Wait();
            //MyAPIPut(cons).Wait();
            //MyAPIDelete(cons).Wait();
            //MyAPIPost(cons).Wait();
        }
        static async Task MyAPIPut(HttpClient cons)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/tblTags/4");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    tblTag tag = await res.Content.ReadAsAsync<tblTag>();
                    tag.tagName = "New Tag";
                    res = await cons.PutAsJsonAsync("api/tblTags/4", tag);
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("------------------Calling Put Operation--------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("tagId    tagName          tagDescription");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("{0}\t{1}\t\t{2}", tag.tagId, tag.tagName, tag.tagDescription);
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");

                    Console.ReadLine();
                }
            }
        }
        static async Task MyAPIDelete(HttpClient cons)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/tblTags/4");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    res = await cons.DeleteAsync("api/tblTags/4");
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("------------------Calling Delete Operation--------------------");
                    Console.WriteLine("------------------Deleted-------------------");
                    Console.ReadLine();
                }
            }
        }
        static async Task MyAPIGet(HttpClient cons)
        {
            using (cons)
            {
                HttpResponseMessage res = await cons.GetAsync("api/tblTags");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    tblTag tag = await res.Content.ReadAsAsync<tblTag>();
                    Console.WriteLine("\n");
                    Console.WriteLine("---------------------Calling Get Operation------------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("tagId    tagName          tagDescription");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("{0}\t{1}\t\t{2}", tag.tagId, tag.tagName, tag.tagDescription);                    
                    Console.ReadLine();
                }
            }
        }
        static async Task MyAPIPost(HttpClient cons)
        {
            using (cons)
            {
                var tag = new tblTag { tagName = "Web API", tagDescription = "This tag is all about Web API" };
                HttpResponseMessage res = await cons.PostAsJsonAsync("api/tblTags", tag);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------------");
                    Console.WriteLine("------------------Calling Post Operation--------------------");
                    Console.WriteLine("------------------Created Successfully--------------------");
                    Console.ReadLine();
                }
            }
        }
    }
    public class tblTag
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagDescription { get; set; }
    }
}
