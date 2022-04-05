using Business.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mvc.WebUI.Infastructure
{
    public static class APIProxy
    {
       public static HttpClient webApiClient = new HttpClient();

       static APIProxy()
        {
            webApiClient.BaseAddress = new Uri("https://localhost:44360/api/");
            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }

}
