using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.Customers;
using EcommerceAPI.Application.DTOs.Orders;
using EcommerceAPI.Application.Features.Queries.Order.GetOrder;
using EcommerceAPI.Application.Features.Queries.Order.GetOrderDetail;
using EcommerceAPI.Application.Repositories.OrderRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, List<GetAllCustomerQueryResponse>>
    {
        readonly ICustomerService _customerService;
        readonly IOrderReadRepository _orderReadRepository;

        public GetAllCustomerQueryHandler(ICustomerService customerService,IOrderReadRepository orderReadRepository)
        {
            _customerService = customerService;
            _orderReadRepository = orderReadRepository;
        }

        public async Task<List<GetAllCustomerQueryResponse>> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var orders =  _orderReadRepository.Table.Include(o => o.AppUser).ToList();
           
            var grouped = orders
                .Where(o => o.AppUser != null)
                .GroupBy(o => o.AppUser)
                .Select(g => new GetAllCustomerQueryResponse
                {
                    Id = g.Key.Id.ToString(),
                    Name = g.Key.UserName,
                    Orders = g.Select(o => new Order_List_DTO
                    {
                        Id = o.Id
                    }).ToList()
                }).ToList();

            return grouped;
        }

    }
    
}
