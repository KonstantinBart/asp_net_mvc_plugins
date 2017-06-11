using System;
using System.Collections.Generic;
using System.IO;

namespace Domain.Core
{
    public interface ISearchParameters
    {
        DateTime CreationDate { get; set; }
        long FileLength { get; set; }
        bool IsSearchInSubfolders { get; set; }
        string FolderPath { get; set; }
		List<FileAttributesForCheckBox> FileAttributes { get; set; }
		String SelectedPluginModule { get; set; }
    }
}
