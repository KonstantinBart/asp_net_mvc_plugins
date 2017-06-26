using Domain.Common;
using Domain.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class SearchHelper
    {

        //internal static async Task<List<String>> AsyncSearchByParameters(ISearchParameters searchParameters, IFinder action, 
        //    CancellationToken token)
        //{
        //    return await Task.Run(() => AsyncSearch(searchParameters, action, token));
        //}

		/// <summary>
		/// Возвращает список файлов.
		/// </summary>
		/// <param name="searchParameters">Параметры поиска.</param>
		/// <returns>Список файлов.</returns>
        internal static List<FileInfo> GetFiles(ISearchParameters searchParameters)
        {
            List<FileInfo> result = new List<FileInfo>();
            try
            {
                var folder = new DirectoryInfo(searchParameters.FolderPath);
                if (!folder.Exists)
                    throw new DirectoryNotFoundException("Folder " + searchParameters.FolderPath + " does not exists");
                return folder.GetFiles("*", searchParameters.IsSearchInSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: {0}", ex.Message);
            }
            return result;
        }

		/// <summary>
		/// Возвращает результат проверки файла на соответствие параметрам поиска.
		/// </summary>
		/// <param name="searchParameters">Параметры поиска.</param>
		/// <param name="action">Действие.</param>
		/// <param name="item">Свойства файла.</param>
		/// <returns>Результат проверки файла.</returns>
        internal static bool CheckFile(ISearchParameters searchParameters, IFinder action, FileInfo item)
        {
            return IsFoundByPlugin(action, item) && CheckByParameters(searchParameters, item);
        }

        //internal static async Task<List<String>> AsyncSearch(ISearchParameters searchParameters, IFinder action,
        //    CancellationToken token)
        //{
        //    List<String> result = new List<String>();

        //    try
        //    {
        //        var folder = new DirectoryInfo(searchParameters.FolderPath);
        //        if (!folder.Exists)
        //            return result;
        //        var files = folder.GetFiles("*", searchParameters.IsSearchInSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        //        foreach (var item in files)
        //        {
        //            if (token.IsCancellationRequested)
        //            {
        //                Debug.WriteLine("Операция прервана. Прочитано {0} файлов", result.Count);
        //                return result;
        //            }
        //            if (await IsFoundByPlugin(action, item) && CheckByParameters(searchParameters, item))
        //                result.Add(item.Name);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine("Exception: {0}", ex.Message);
        //    }
        //    return result;
        //}

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

		/// <summary>
		/// Заполняет начальные параметры формы.
		/// </summary>
		/// <returns>Список параметров.</returns>
		internal static ISearchParameters FillSearchParameters()
		{
			ISearchParameters searchParameters = new SearchParameters();
			searchParameters.FolderPath = @"C:\Test\";
			searchParameters.IsSearchInSubfolders = true;
			searchParameters.FileLength = 10 * 1024;
			searchParameters.CreationDate = DateTime.Now.Date;

			List<FileAttributesForCheckBox> fileAttributesList = new List<FileAttributesForCheckBox>();
			foreach (var item in Enum.GetValues(typeof(FileAttributes)))
			{
				fileAttributesList.Add(new FileAttributesForCheckBox { Text = item.ToString(), Value = Convert.ToInt32(item), IsChecked = true });
			}
			searchParameters.FileAttributes = fileAttributesList;

			return searchParameters;
		}
	}
}