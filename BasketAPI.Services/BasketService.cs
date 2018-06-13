using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BasketAPI.Interfaces;
using BasketAPI.Models;

namespace BasketAPI.Services
{
    public class BasketService : IBasketService
    {
        public BasketModel CreateNewBasket(string clientId)
        {
            if(Config.GetClients().Exists(x => x.Id == clientId))
            {
                BasketModel basketModel = new BasketModel();
                basketModel.Id = Guid.NewGuid().ToString();
                basketModel.Client = Config.GetClients().Find(x => x.Id == clientId);

                Config.SaveBasketData(basketModel);

                return basketModel;
            }

            return null;

        }

        public bool AddBasketItem(string clientId, string basketId, BasketItemModel basketItemModel)
        {
            bool isAdded = false;

            //Check if all mandatory fields are present
            if (String.IsNullOrEmpty(basketItemModel.Id) || String.IsNullOrEmpty(basketItemModel.Name) || basketItemModel.UnitPrice < 0
                    || basketItemModel.Quantity < 1)
            {
                isAdded = false;
            }
            else
            {
                if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
                {
                    BasketModel basketRepModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                    //This maybe the first items being added to the 
                    if (basketRepModel.BasketItems == null)
                    {
                        basketRepModel.BasketItems = new List<BasketItemModel>();
                    }
                    //If the item already exists, call the UpdateItemQuantity API not this one
                    if (basketRepModel.BasketItems.Exists(x=> x.Id == basketItemModel.Id))
                    {
                        isAdded = false;
                    }
                    else
                    {
                        basketItemModel.SubTotal = CalculateSubTotal(basketItemModel.UnitPrice, basketItemModel.Quantity, basketItemModel.Discount);
                        basketRepModel.BasketItems.Add(basketItemModel);
                        isAdded = true;
                    }
                }
            }

            return isAdded;
        
        }

        public bool RemoveBasketItem(string clientId, string basketId, string itemId)
        {
            bool isRemoved = false;

            if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
            {
                BasketModel basketRepModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                BasketItemModel basketItemModel = basketRepModel.BasketItems.Find(x=>x.Id == itemId);
                if(basketItemModel != null)
                {
                    basketRepModel.BasketItems.Remove(basketItemModel);
                    isRemoved = true;
                }
            }

            return isRemoved;

        }

        public bool UpdateBasketItemQuantity(string clientId, string basketId, string itemId, int quantity)
        {
            bool isUpdated = false;

            if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
            {
                BasketModel basketRepModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                BasketItemModel basketItemModel = basketRepModel.BasketItems.Find(x => x.Id == itemId);
                if (basketItemModel != null)
                {
                    basketItemModel.Quantity = quantity;
                    basketItemModel.SubTotal = CalculateSubTotal(basketItemModel.UnitPrice, basketItemModel.Quantity, basketItemModel.Discount);
                    isUpdated = true;
                }
            }

            return isUpdated;

        }

        public bool ClearBasket(string clientId, string basketId)
        {
            bool isCleared = false;

            if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
            {
                BasketModel basketRepModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                basketRepModel.BasketItems.Clear();

                isCleared = true;
            }

            return isCleared;

        }

        public bool RemoveBasket(string clientId, string basketId)
        {
            bool isRemoved = false;

            if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
            {
                BasketModel basketRepModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                Config.BasketData.Remove(basketRepModel);

                isRemoved = true;
            }

            return isRemoved;

        }

        public BasketModel GetBasket(string clientId, string basketId)
        {
            BasketModel basketModel = null;

            if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
            {
                basketModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                basketModel.TotalBasketPrice = (float)Math.Round(basketModel.BasketItems.Sum(x => x.SubTotal), 2);
            }

            return basketModel;
        }

        private float CalculateSubTotal(float unitPrice, int quantity, float discount)
        {
            if(discount > 0)
            {
                float discountedPrice = (float)Math.Round((unitPrice * quantity) - (unitPrice * discount / 100), 2);

                if (discountedPrice >= 0) return discountedPrice;
                else return 0;
            }
            else
            {
                return (float)Math.Round(unitPrice * quantity, 2);
            }
        }
    }
}
