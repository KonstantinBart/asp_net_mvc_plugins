using System;
using System.IO;

namespace Domain.Core
{
    public interface ISearchParameters
    {
        DateTime CreationDate { get; set; }
        FileAttributes FileAttributes { get; set; }
        long FileLength { get; set; }
        bool IsSearchInSubfolders { get; set; }
        string FolderPath { get; set; }
        //PluginsInitializer.FileExtension FileExtension {get; set;}
        ISearchFileParameters searchFileParameters { get; set; }
    }
}
