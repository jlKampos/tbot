using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TBotParsers
{
	public class GoogleSearchResults : IGoogleSearchResults
	{
		double searchTime;
		long totalResults;
		List<GoogleSearchStruct> results;

		public GoogleSearchResults()
		{
			searchTime = 0;
			totalResults = 0;
			results = new List<GoogleSearchStruct>();
		}

		public double SearchTime
		{
			get { return searchTime; }
			set { searchTime = value; }
		}

		public long TotalResults
		{
			get { return totalResults; }
			set { totalResults = value; }
		}


		public List<GoogleSearchStruct> Results
		{
			get { return results; }
		}


		public bool AddResult(GoogleSearchStruct dump)
		{
			results.Add(dump);
			results = results.Distinct().ToList();
			return true;
		}
	}
}
