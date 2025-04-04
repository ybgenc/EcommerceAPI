﻿using EcommerceAPI.Application.Abstraction.Hubs.ProductHub;
using EcommerceAPI.Application.Repositories.ProductRepository;
using MediatR;

namespace EcommerceAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;
        readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new() { Name = request.Name, Price = request.Price, Stock = request.Stock, Description = request.Description, Title = request.Title });
            await _productWriteRepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($" new product {request.Name} added successfully");
            return new();
        }
    }
}
