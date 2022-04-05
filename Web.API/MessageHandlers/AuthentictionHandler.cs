using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Web.API.MessageHandlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = request.Headers.GetValues("").FirstOrDefault();
                if (token !=null)
                {
                    byte[] data = Convert.FromBase64String(token);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokenValues = decodedString.Split(":");

                    if (tokenValues[0]=="" &&tokenValues[1]=="12345")
                    {
                        
                    }
                }
            }
            catch
            {
                
            }
            return base.SendAsync(request, cancellationToken);  
        }
    }
}
