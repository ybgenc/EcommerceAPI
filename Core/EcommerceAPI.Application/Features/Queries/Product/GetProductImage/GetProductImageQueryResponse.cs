namespace EcommerceAPI.Application.Features.Queries.Product.GetProductImage
{
    public class GetProductImageQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public bool ShowCase { get; set; }
        public Guid Id { get; set; }

    }
}
