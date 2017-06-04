using System;
using System.Xml;
using Domain.Common;
using System.Threading.Tasks;

namespace XmlFinderPlugin
{
	public class Finder : IFinder
	{
		public string FileExtension
		{
			get { return ".xml"; }
		}

        public async Task<bool> Find(String fileName)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fileName);
				XmlNode root = doc.DocumentElement;
				var lst = root.SelectNodes(".//text()");

				foreach (XmlNode item in lst)
				{
					if (item.Value.Equals(SearchPattern))
						return true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The XML file could not be read.");
			}
			return false;
		}

		public string SearchPattern
		{
			get { return "The Sundered Grail"; }
		}

	}
}
