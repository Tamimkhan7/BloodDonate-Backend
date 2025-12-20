namespace BloodBankAPI.Models
{
    public class SearchResult
    {
        public List<DonorWithDistance> Donors { get; set; } = new List<DonorWithDistance>();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
