using Common;

namespace TxtFinderPlugin
{
    public class Finder : IFinder
    {
        public string Find(object condition)
        {
            return "Try to find by condition...";
        }

        public string Name
        {
            get { return ".txt"; }
        }
    }
}

