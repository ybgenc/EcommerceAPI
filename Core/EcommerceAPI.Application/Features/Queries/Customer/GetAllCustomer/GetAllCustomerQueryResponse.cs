using EcommerceAPI.Application.DTOs.Orders;

namespace EcommerceAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Order_List_DTO> Orders { get; set; }
    }
}