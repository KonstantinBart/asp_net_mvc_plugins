using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Domain.Core
{
    public class SearchParameters : ISearchParameters
    {
        [Required(ErrorMessage = "Пожалуйста, введите путь для поиска")]
        public string FolderPath { get; set; }

        public string FileName { get; set; }

        public bool IsSearchInSubfolders { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите строку для поиска")]
        public string SearchPattern { get; set; }

        public long FileLength { get; set; }

        public DateTime CreationDate { get; set; }

        public FileAttributes FileAttributes { get; set; }

        //public PluginsInitializer.FileExtension FileExtension { get; set; }

        public ISearchFileParameters searchFileParameters { get; set; }
    }
}
