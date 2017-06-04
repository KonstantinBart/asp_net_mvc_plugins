using Domain.Common;
using System;
using System.Threading.Tasks;

namespace DocFinderPlugin
{
    public class Finder : IFinder
    {
        public string FileExtension
        {
            get { return ".doc"; }
        }

        public async Task<bool> Find(String fileName)
		{
			throw new System.NotImplementedException();
		}

        public string SearchPattern
        {
            get { return "doc search pattern"; }
        }
    }
}

