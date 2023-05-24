namespace HMZ.DTOs.Queries.Base
{
    public class BaseQuery<T>
    {
        public Int32? PageNumber { get; set; }
        public Int32? PageSize { get; set; }
        public T? Entity { get; set; }
        public String? Username { get; set; }
        public List<String>? SortColums { get; set; }
        public int TotalItems { get; set; }
    }
}

