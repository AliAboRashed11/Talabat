namespace Talabat.Core.ISpecifications
{
    public class ProductSpecParms
    {
        



        private int pageSiza = 5;
        public int PageSiza
        {
            get { return pageSiza; }
            set { pageSiza = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;

        private string search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }



    }    
}
