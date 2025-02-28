using EcommerceAPI.Application.DTOs.Baskets;
using EcommerceAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Abstraction.Services
{
    public interface IBasketService 
    {
        public Task<List<BasketItem>> GetBasketItemAsync();
        public Task AddItemToBasketAsync(Create_BasketItem_DTO basketItem);
        public Task UpdateBasketItemAsync(Update_BasketItem_DTO basketItem);
        public Task DeleteBasketItemAsync(string basketItemId);
        public Basket? GetBasketId { get; }
    }
}
