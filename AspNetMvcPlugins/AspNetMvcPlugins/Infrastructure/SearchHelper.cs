using Domain.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace AspNetMvcPlugins.Infrastructure
{
    public static class SearchHelper
    {
        public static void TestSearch()
        {
            //String path = @"C:\test\";
            //SearchOption searchOption = SearchOption.AllDirectories;
            //String searchPattern = "*";
            //long fileLength = 2048;
            //DateTime creationDate = DateTime.Now;
            //FileAttributes fileAttributes = FileAttributes.Normal;

            ISearchParameters searchParameters = new SearchParameters();
            searchParameters.FolderPath = @"C:\test\";
            searchParameters.IsSearchInSubfolders = true;
            searchParameters.FileLength = 2048;
            searchParameters.CreationDate = DateTime.Now;
            searchParameters.FileAttributes = FileAttributes.Normal;


            ISearchFileParameters fileParameters1 = new SearchFileParameters();
            fileParameters1.FileName = @"c:\test\254.txt";
            fileParameters1.SearchPattern = "test string";

            searchParameters.searchFileParameters = fileParameters1;
            //searchParameters.FileExtension = FileExtension.Txt;
            TestIt(searchParameters);

            ISearchFileParameters fileParameters2 = new SearchFileParameters();
            fileParameters2.FileName = @"C:\test\1234.xml";
            fileParameters2.SearchPattern = "The Sundered Grail";

            searchParameters.searchFileParameters = fileParameters2;
            //searchParameters.FileExtension = FileExtension.Xml;
            TestIt(searchParameters);
        }

        private static bool HasInTxt(ISearchFileParameters searchParameters)
        {
            //String path = @"c:\test\254.txt";
            //String findString = "test string";
            try
            {
                using (StreamReader sr = new StreamReader(searchParameters.FileName))
                {
                    String fileContents = sr.ReadToEnd();
                    if (fileContents.Contains(searchParameters.SearchPattern))
                        return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The TXT file could not be read.");
            }
            return false;
        }


        private static bool HasInXml(ISearchFileParameters searchParameters)
        {
            //String path = @"C:\test\1234.xml";
            //String findString = "The Sundered Grail";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(searchParameters.FileName);
                XmlNode root = doc.DocumentElement;
                var lst = root.SelectNodes(".//text()");

                foreach (XmlNode item in lst)
                {
                    if (item.Value.Equals(searchParameters.SearchPattern))
                        return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The XML file could not be read.");
            }
            return false;
        }

        private static void TestIt(ISearchParameters searchParameters)
        {
            //String path = @"C:\test\";
            //SearchOption searchOption = SearchOption.AllDirectories;
            //String searchPattern = "*";
            //long fileLength = 2048;
            //DateTime creationDate = DateTime.Now;
            //FileAttributes fileAttributes = FileAttributes.Normal;

            List<String> result = new List<String>();
            var folder = new DirectoryInfo(searchParameters.FolderPath);
            if (!folder.Exists)
                return;
            var files = folder.GetFiles("*", searchParameters.IsSearchInSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            foreach (var item in files)
            {
                searchParameters.searchFileParameters.FileName = item.FullName;
                bool isFoundInPlugin = false;
                switch (item.Extension)
                {
                    case ".txt":
                        isFoundInPlugin = HasInTxt(searchParameters.searchFileParameters);
                        break;
                    case ".xml":
                        isFoundInPlugin = HasInXml(searchParameters.searchFileParameters);
                        break;
                }

                if (isFoundInPlugin && (item.Length < searchParameters.FileLength || item.CreationTime < searchParameters.CreationDate || item.Attributes == searchParameters.FileAttributes))
                    result.Add(item.Name);
            }
        }

        //[Flags]
        //public enum FileExtension
        //{
        //    // The flag is 0001.
        //    Txt = 0x01,
        //    // The flag is 0010.
        //    Xml = 0x02,
        //    // The flag is 0100.
        //    Doc = 0x04,
        //    // The flag is 1000.
        //    Log = 0x08
        //}
    }
}