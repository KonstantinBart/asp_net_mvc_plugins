using System;
using System.Collections.Generic;

namespace Domain.Core
{
    public class SearchParameters : ISearchParameters
    {
        public string FolderPath { get; set; }

        public bool IsSearchInSubfolders { get; set; }

        public long FileLength { get; set; }

		public DateTime CreationDate { get; set; }

		public List<FileAttributesForCheckBox> FileAttributes { get; set; }

		public string SelectedPluginModule { get; set; }
	}
}
