using System;
using System.IO;
using Domain.Common;

namespace TxtFinderPlugin
{
    public class Finder : IFinder
    {
        public string FileExtension
        {
            get { return ".txt"; }
        }

		public bool Find(String fileName)
		{
			try
			{
				using (StreamReader sr = new StreamReader(fileName))
				{
					String fileContents = sr.ReadToEnd();
					if (fileContents.Contains(SearchPattern))
						return true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("The TXT file could not be read.");
			}
			return false;
		}

		public string SearchPattern
		{
			get { return "test string"; }
		}

	}
}

