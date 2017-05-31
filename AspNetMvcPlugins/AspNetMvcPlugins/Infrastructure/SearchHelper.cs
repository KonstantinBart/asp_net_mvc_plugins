using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Domain.Core;
using System.Linq;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class SearchHelper
    {
		internal static IEnumerable<String> TestSearch(ISearchParameters searchParameters, IFinder action)
		{
			List<String> result = new List<String>();

			var folder = new DirectoryInfo(searchParameters.FolderPath);
			if (!folder.Exists)
				return result;
			var files = folder.GetFiles("*", searchParameters.IsSearchInSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
			foreach (var item in files)
			{
				bool isFoundInPlugin = (action == null);

				if (action != null && action.FileExtension.Equals(item.Extension))
					isFoundInPlugin = action.Find(item.FullName);

				var checkedFileAttributes = from res in searchParameters.FileAttributes where res.IsChecked select res.Value;
				//foreach (var attribute in checkedFileAttributes)
				//{
				//	if(Enum.IsDefined(typeof(FileAttributes), attribute))
				//		fileAttributes &= (FileAttributes)attribute;
				//}

				//FileAttributes erw = (FileAttributes.Hidden & FileAttributes.IntegrityStream);
				if (isFoundInPlugin && (item.Length < searchParameters.FileLength && item.CreationTime < searchParameters.CreationDate) )
					//&& item.Attributes == fileAttributes)
					result.Add(item.Name);
			}
			return result;
						
		}
	}
}