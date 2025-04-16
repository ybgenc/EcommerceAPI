using EcommerceAPI.Application.Abstraction.Services;
using EcommerceAPI.Application.DTOs.Baskets;
using EcommerceAPI.Application.Repositories.BasketItemRepository;
using EcommerceAPI.Application.Repositories.BasketRepository;
using EcommerceAPI.Application.Repositories.OrderRepository;
using EcommerceAPI.Domain.Entities;
using EcommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Persistence.Services
{
    public class BasketService : IBasketService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly UserManager<AppUser> _userManager;
        readonly IOrderReadRepository _orderReadRepository;

        readonly IBasketWriteRepository _basketWriteRepository;
        readonly IBasketReadRepository _basketReadRepository;

        readonly IBasketItemReadRepository _basketItemReadRepository;
        readonly IBasketItemWriteRepository _basketItemWriteRepository;


        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderReadRepository orderReadRepository, IBasketWriteRepository basketWriteRepository, IBasketItemReadRepository basketItemReadRepository, IBasketItemWriteRepository basketItemWriteRepository, IBasketReadRepository basketReadRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderReadRepository = orderReadRepository;
            _basketWriteRepository = basketWriteRepository;
            _basketReadRepository = basketReadRepository;

            _basketItemReadRepository = basketItemReadRepository;
            _basketItemWriteRepository = basketItemWriteRepository;
        }

        private async Task<Basket?> getUserBasket()
        {
            var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;

            if (!string.IsNullOrEmpty(user))
            {
                AppUser? username = await _userManager.Users.Include(u => u.Baskets).FirstOrDefaultAsync(u => u.UserName == user);
                var _basket = from basket in username.Baskets
                              join order in _orderReadRepository.Table
                              on basket.Id equals order.Id into BasketsOrder
                              from order in BasketsOrder.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };
                Basket? activeBasket = null;
                if (_basket.Any(b => b.Order == null))
                    activeBasket = _basket.FirstOrDefault(b => b.Order == null)?.Basket;
                else
                {
                    activeBasket = new();
                    username.Baskets.Add(activeBasket);

                }
                await _basketWriteRepository.SaveAsync();
                return activeBasket;

            }
            throw new Exception("getUserBasket() Error"); // TODO: Get User Basket Exception

        }

        public async Task AddItemToBasketAsync(Create_BasketItem_DTO basketItem)
        {
            Basket? basket = await getUserBasket();
            if (basket != null)
            {
                BasketItem? existItem = await _basketItemReadRepository.GetSingleAsync(item => item.BasketId == basket.Id &&
                                                                                               item.ProductId == Guid.Parse(basketItem.ProductId));
                if (existItem != null)
                {
                    existItem.Quantity++;
                }
                else
                {
                    await _basketItemWriteRepository.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,
                    });
                }
            }
        }

        public async Task DeleteBasketItemAsync(string basketItemId)
        {

            BasketItem product = await _basketItemReadRepository.GetByIdAsync(basketItemId);
            if (product != null)
            {

                await _basketItemWriteRepository.DeleteAsync(basketItemId);
            }
            await _basketItemWriteRepository.SaveAsync();


        }

        public async Task<List<BasketItem>> GetBasketItemAsync()
        {
            Basket? basket = await getUserBasket();
            Basket? getBasketItems = await _basketReadRepository.Table
                            .Include(b => b.BasketItems)
                            .ThenInclude(bi => bi.Product)
                            .FirstOrDefaultAsync(b => b.Id == basket.Id);


            return getBasketItems?.BasketItems?.ToList();
        }

        public async Task UpdateBasketItemAsync(Update_BasketItem_DTO basketItem)
        {
            BasketItem? existItem = await _basketItemReadRepository.GetByIdAsync(basketItem.BasketItemId);

            if (existItem == null)
                throw new Exception("UpdateBasketItemAsync() Error"); // TODO: Basket Item Update Exception

            if (basketItem.Quantity == 0)
            {
                await _basketItemWriteRepository.DeleteAsync(basketItem.BasketItemId);
            }
            else
            {
                existItem.Quantity = basketItem.Quantity;
            }

            await _basketItemWriteRepository.SaveAsync();
        }

        public Basket? GetBasketId
        {
            get
            {
                Basket? basket = getUserBasket().Result;
                return basket;
            }
        }





    }
}