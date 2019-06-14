using System;

namespace TBotParsers
{
	public class GoogleSearchStruct
	{
		string title;
		string link;

		public GoogleSearchStruct()
		{
			title = null;
			link = null;
		}

		public GoogleSearchStruct(string title, string link)
		{
			this.title = title;
			this.link = link;
		}

		public string Title
		{
			get { return title; }
			set
			{
				if (value != title)
				{
					title = value;
				}
			}
		}

		public string Link
		{
			get { return link; }
			set
			{
				if (value != link)
				{
					link = value;
				}
			}
		}
	}
}
