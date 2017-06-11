using System;
using System.Xml;
using Domain.Common;
using System.Threading.Tasks;
using System.Diagnostics;

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
				Debug.WriteLine("The XML file could not be read. {0}", e.Message);
			}
			return false;
		}

		public string SearchPattern
		{
			get { return "The Sundered Grail"; }
		}

	}
}
