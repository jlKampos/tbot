using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TExceptions;

namespace TBotParsers
{
	public class GoogleJsonParser
	{

		public GoogleSearchResults ConvertJsonToGoogleResults(JObject json)
		{
			try
			{
				GoogleSearchResults results = new GoogleSearchResults();

				if (json == null || json.Count == 0)
				{
					return null;
				}

				GoogleSearchResults gResult = new GoogleSearchResults();

				JToken searchInfo = json["searchInformation"];
				gResult.SearchTime = (double)searchInfo["searchTime"];
				gResult.TotalResults = (long)searchInfo["totalResults"];

				JToken gResults = json["items"];

				if (gResults == null)
				{
					throw new ExceptionGoogleResultItemsNULL((string)json.SelectToken("queries.request").First().Value<string>("searchTerms"));
				}

				foreach (var item in gResults)
				{
					GoogleSearchStruct dump = new GoogleSearchStruct();

					dump.Title = item.Value<string>("title");
					dump.Link = item.Value<string>("link");

					results.AddResult(dump);

				}

				return results;
			}
			catch (ExceptionGoogleResultItemsNULL ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{

				throw new ExceptionGoogleParser(ex.Message);
			}
		}
	}
}
