using System;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;



namespace AllenXamarinTest
{

	public partial class Default : System.Web.UI.Page
	{
		

	protected void Page_Load(object sender, EventArgs e)
	{

			log("Thisapp", "Received a call");


				var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://fullback.cfapps.pez.pivotal.io"));

            string responseString;

            string traceid = Request.Headers["X-B3-TraceId"];
           

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://fullback.cfapps.pez.pivotal.io"));
            request.Headers.Add("X-B3-Traceid", traceid);
            request.Headers.Add("X-B3-SpanId", "11111");
            string data;

            using (client = new HttpClient())
            {
                HttpResponseMessage response = client.SendAsync(request).Result;
                data = response.Content.ReadAsStringAsync().Result;
                responseString = data;
            }

            Console.WriteLine("Data from response ....  " + responseString);
        }

		public void log(string appname, string logmessage)
		{
			//if the request had a traceid then grab trace and span id and mark up the log with zipkin output formatting
			if (Request.Headers.GetValues("X-B3-TraceId") != null)
			{

				string headeri = Request.Headers ["X-B3-TraceId"];
				string spani = Request.Headers ["X-B3-SpanId"];
				//The above returns an array even though we only get one per request so using the linq firstordefault to get the value
				

				Console.WriteLine("[" + appname + ", " + headeri + ", " + spani + ", true] " + logmessage);
			}
			else
			{
				//If no traceid just produce a log
				Console.WriteLine("[" + appname + ", " + logmessage);
			}

		}
		//go get the traceid value from the request
		private string traceidval() 
		{
            
            //if (Request.Headers.GetValues("X-B3-TraceId") != null)
            //{
            string traceid = Request.Headers ["X-B3-TraceId"];
				//IEnumerable<string> spanheaders = Request.Headers.GetValues("X-B3-SpanId");
				//The above returns an array even though we only get one per request so using the linq firstordefault to get the value
				
				return traceid;
			//}
			//else

			//{
				//return null; 
			//}
		}

}


	}
