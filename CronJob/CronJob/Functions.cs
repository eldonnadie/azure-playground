using Microsoft.Azure.WebJobs;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Web;

namespace CronJob
{
	public class Functions
	{
		// This function will get triggered/executed when a new message is written 
		// on an Azure Queue called queue.
		public void ProcessQueueMessage([QueueTrigger("hectortestqueue")] string message, TextWriter log)
		{
			log.WriteLine("entry process queue");

			string pingSiteAppValue = ConfigurationManager.AppSettings["pingSite"];
			string pingSiteUrl = HttpUtility.UrlDecode(pingSiteAppValue);
			var httpClient = new HttpClient();
			MakeRequest(httpClient, pingSiteUrl, log);

			log.WriteLine(message);
		}

		private async void MakeRequest(HttpClient httpClient, string url, TextWriter logger)
		{
			var response = await httpClient.GetAsync(url);
			logger.WriteLine("all processed");
		}
	}
}
