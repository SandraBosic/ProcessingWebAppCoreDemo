namespace Contracts.ViewModels
{
    public class PaginationViewModel
    {
        public int TotalCount { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public bool IsPreviousPage { get; set; }

        public bool IsNextPage { get; set; }
        public string PaginationFunctionUrl { get; set; }
    }
}
