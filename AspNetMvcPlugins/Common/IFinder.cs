using System;

namespace Common
{
    public interface IFinder
    {
        String Find(Object condition);
        String Name { get; }
    }
}
