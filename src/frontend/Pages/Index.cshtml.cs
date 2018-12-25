using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace frontend.Pages
{
    public class IndexModel : PageModel
    {
     public string BackendMessage { get; set; }
        public void OnGet()
        {
            var host = Environment.GetEnvironmentVariable("BACKEND_HOST");
            var port = Environment.GetEnvironmentVariable("BACKEND_PORT");
            using(var resp = new System.Net.WebClient().OpenRead($"http://{host}:{port}/api/values")){
                using(var sr = new System.IO.StreamReader(resp)){
                    BackendMessage = $"Message from backend : {sr.ReadToEnd()}";
                }
            }
        }
    }
}
