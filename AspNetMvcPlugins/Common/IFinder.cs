using System;
using System.IO;

namespace Domain.Common
{
    public interface IFinder
    {
        bool Find(String fileName);
        String FileExtension { get; }
		String SearchPattern { get; }
    }
}
