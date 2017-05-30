using System;
using System.IO;
using Domain.Core;

namespace Common
{
    public interface IFinder
    {
        bool Find(String fileName);
        String FileExtension { get; }
		String SearchPattern { get; }
    }
}
