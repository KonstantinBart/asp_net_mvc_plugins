﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IFinder
    {
        Task<bool> Find(String fileName);
        String FileExtension { get; }
		String SearchPattern { get; }
    }
}
