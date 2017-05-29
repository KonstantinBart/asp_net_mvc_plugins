namespace Domain.Core
{
    public interface ISearchFileParameters
    {
        string FileName { get; set; }
        string SearchPattern { get; set; }
    }
}
