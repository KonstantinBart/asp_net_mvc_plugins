using Common;

namespace DocFinderPlugin
{
    public class Finder : IFinder
    {
        public string Name
        {
            get { return ".doc"; }
        }

		public bool Find(ISearchFileParameters searchParameters)
		{
			throw new System.NotImplementedException();
		}
    }
}

