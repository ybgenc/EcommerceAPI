using EcommerceAPI.Application.ViewModels.Basket;
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
        public Task AddItemToBasketAsync(VM_Create_BasketItems basketItem);
        public Task UpdateBasketItemAsync(VM_Update_BasketItem basketItem);
        public Task DeleteBasketItemAsync(string basketItemId);
    }
}
