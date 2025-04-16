namespace EcommerceAPI.Application.Features.Queries.Order.GetCustomerOrders
{
    public class GetCustomerOrdersQueryResponse
    {
        public string Description { get; set; }
        public string OrderNumber { get; set; }
        public bool isSended { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}