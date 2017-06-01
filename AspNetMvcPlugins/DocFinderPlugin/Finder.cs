using Domain.Common;
using System;

namespace DocFinderPlugin
{
    public class Finder : IFinder
    {
        public string FileExtension
        {
            get { return ".doc"; }
        }

        public bool Find(String fileName)
		{
			throw new System.NotImplementedException();
		}

        public string SearchPattern
        {
            get { return "doc search pattern"; }
        }
    }
}

