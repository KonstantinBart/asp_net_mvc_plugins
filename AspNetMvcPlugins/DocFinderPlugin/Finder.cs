using Common;

namespace DocFinderPlugin
{
    public class Finder : IFinder
    {
        public string Find(object condition)
        {
            return "Try to find by condition...";
        }

        public string Name
        {
            get { return ".doc"; }
        }
    }
}

