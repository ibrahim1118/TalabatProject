namespace Talabt.APIs.Helper
{
    public class ProductSpacPrams
    {
        public int PageIndex { get; set; } = 1;

        private int pageSize =5;


        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = Math.Min(value ,10); }
        }
         

        public string? sort { get; set; }

        public int? BrandId { get; set; }
        public int? Typeid { get; set; }

        public string? search { get; set; }
    }
}
