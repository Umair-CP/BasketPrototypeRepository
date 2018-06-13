using BasketAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Interfaces
{
    public interface IBasketService
    {
        BasketModel CreateNewBasket(string clientId);
        bool AddBasketItem(string clientId, string basketId, BasketItemModel basketItemModel);
        bool RemoveBasketItem(string clientId, string basketId, string itemId);
        bool UpdateBasketItemQuantity(string clientId, string basketId, string itemId, int quantity);
        bool ClearBasket(string clientId, string basketId);
        bool RemoveBasket(string clientId, string basketId);
        BasketModel GetBasket(string clientId, string basketId);
    }
}
