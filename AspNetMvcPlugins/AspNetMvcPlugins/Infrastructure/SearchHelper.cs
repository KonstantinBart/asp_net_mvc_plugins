using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domain.Common;
using Domain.Core;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class SearchHelper
    {
		internal static IEnumerable<String> TestSearch(ISearchParameters searchParameters, IFinder action)
		{
			List<String> result = new List<String>();

			try
			{
				var folder = new DirectoryInfo(searchParameters.FolderPath);
				if (!folder.Exists)
					return result;
				var files = folder.GetFiles("*", searchParameters.IsSearchInSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
				foreach (var item in files)
				{
					if (IsFoundByPlugin(action, item) && CheckByParameters(searchParameters, item))
						result.Add(item.Name);
				}
			}
			catch (Exception ex)
			{
				//TODO: Add to log...
			}
			return result;
		}

		private static bool IsFoundByPlugin(IFinder action, FileInfo item)
		{
			bool isFoundInPlugin = (action == null);

			if (action != null && action.FileExtension.Equals(item.Extension))
				isFoundInPlugin = action.Find(item.FullName);
			return isFoundInPlugin;
		}

		private static bool CheckByParameters(ISearchParameters searchParameters, FileInfo item)
		{
			var checkedFileAttributes = from res in searchParameters.FileAttributes where res.IsChecked select res.Value;
			bool hasAllFileAttributes = Enum.GetValues(typeof(FileAttributes)).Cast<FileAttributes>().
				Where(x => item.Attributes.HasFlag(x)).Cast<int>().
				All(x => checkedFileAttributes.Contains(x));

			return item.Length < searchParameters.FileLength && 
				item.CreationTime < searchParameters.CreationDate && 
				hasAllFileAttributes;
		}

		internal static ISearchParameters FillSearchParameters()
		{
			ISearchParameters searchParameters = new SearchParameters();
			searchParameters.FolderPath = @"C:\test\";
			searchParameters.IsSearchInSubfolders = true;
			searchParameters.FileLength = 10 * 1024;
			searchParameters.CreationDate = DateTime.Now.Date;

			List<FileAttributesForCheckBox> fileAttributesList = new List<FileAttributesForCheckBox>();
			foreach (var item in Enum.GetValues(typeof(FileAttributes)))
			{
				fileAttributesList.Add(new FileAttributesForCheckBox { Text = item.ToString(), Value = Convert.ToInt32(item), IsChecked = false });
			}
			searchParameters.FileAttributes = fileAttributesList;

			return searchParameters;
		}
	}
}