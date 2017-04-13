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
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://fullback.cfapps.pez.pivotal.io"));
				
				//string headeri = traceidval();
				//You have to add both a traceid and spanid header, otherwise Gorouter overwrites the traceID, it doesn't really matter what spanid you use as the router will replace with it's own span identifier in current release
				request.Headers.Add("X-B3-TraceId", "DotnetAPPTraceID");
				request.Headers.Add("X-B3-SpanId", "CALLSPANID");
            Console.WriteLine("565656" + (request.Headers.ToString()));

            //sending the call
            HttpResponseMessage  response = client.SendAsync(request).Result;

				//this logs the call result.  This won't show up in trace explorer as it misses the [app, traceid, spanid, true] formatting 
				

            string data = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Data from response " + data);
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
