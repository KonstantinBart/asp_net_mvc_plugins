using System.ComponentModel.DataAnnotations;

namespace Domain.Core
{
    public class SearchFileParameters : ISearchFileParameters
    {
        public string FileName { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите строку для поиска")]
        public string SearchPattern { get; set; }
    }
}
