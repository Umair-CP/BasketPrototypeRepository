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

        /// <summary>
        /// This method creates a new basket for a client.
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <returns>Basket Model</returns>
        public BasketModel CreateNewBasket(string clientId)
        {
            //Check if the client is registered
            if(Config.GetClients().Exists(x => x.Id == clientId))
            {
                BasketModel basketModel = new BasketModel();
                //Assign a new ID for the new basket
                basketModel.Id = Guid.NewGuid().ToString();
                basketModel.Client = Config.GetClients().Find(x => x.Id == clientId);

                Config.SaveBasketData(basketModel);

                return basketModel;
            }

            return null;

        }


        /// <summary>
        /// This method adds a new item into the basket
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <param name="basketItemModel">Basket items model for item being added</param>
        /// <returns>True if add item was successful otherwise false</returns>
        public bool AddBasketItem(string clientId, string basketId, BasketItemModel basketItemModel)
        {
            bool isAdded = false;

            //Check if all mandatory fields are present, unit price of item is not less than 0 and quantity is more than 1
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
                    //This maybe the first items being added, therefore initialise the items collection here 
                    if (basketRepModel.BasketItems == null)
                    {
                        basketRepModel.BasketItems = new List<BasketItemModel>();
                    }
                    //If the item already exists, call the UpdateItemQuantity API not this method
                    if (basketRepModel.BasketItems.Exists(x=> x.Id == basketItemModel.Id))
                    {
                        isAdded = false;
                    }
                    else
                    {
                        //Calculate the sub-total price of this item based on unit price and quantity
                        basketItemModel.SubTotal = CalculateSubTotal(basketItemModel.UnitPrice, basketItemModel.Quantity, basketItemModel.Discount);
                        basketRepModel.BasketItems.Add(basketItemModel);
                        isAdded = true;
                    }
                }
            }

            return isAdded;
        
        }


        /// <summary>
        /// This method removes a specific item based on ID
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemId">Item ID of item to be removed</param>
        /// <returns>True if remove item was successful otherwise false</returns>
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


        /// <summary>
        /// This method updates the quantity of an existing item in basket and re-calculates sub-total 
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemId">Item ID of basket item</param>
        /// <param name="quantity">Quantity of item</param>
        /// <returns>True if update item quantity was successful otherwise false</returns>
        public bool UpdateBasketItemQuantity(string clientId, string basketId, string itemId, int quantity)
        {
            bool isUpdated = false;

            if (quantity > 0)
            {
                if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
                {
                    BasketModel basketRepModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                    BasketItemModel basketItemModel = basketRepModel.BasketItems.Find(x => x.Id == itemId);
                    if (basketItemModel != null)
                    {
                        basketItemModel.Quantity = quantity;

                        //Calculate the new sub-total for this item
                        basketItemModel.SubTotal = CalculateSubTotal(basketItemModel.UnitPrice, basketItemModel.Quantity, basketItemModel.Discount);
                        isUpdated = true;
                    }
                }
            }

            return isUpdated;

        }


        /// <summary>
        /// This method clears basket items but does not delete the basket
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <returns>True if clear basket was successful otherwise false</returns>
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


        /// <summary>
        /// This method deletes the basket
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <returns>True if remove basket was successful otherwise false</returns>
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


        /// <summary>
        /// This method retrieves the entire basket and also calculates the total price of all items
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <returns>Basket Model</returns>
        public BasketModel GetBasket(string clientId, string basketId)
        {
            BasketModel basketModel = null;

            if (Config.BasketData.Exists(x => x.Client.Id == clientId && x.Id == basketId))
            {
                basketModel = Config.BasketData.Find(x => x.Client.Id == clientId && x.Id == basketId);
                //Calculate the total price of all the items
                basketModel.TotalBasketPrice = (float)Math.Round(basketModel.BasketItems.Sum(x => x.SubTotal), 2);
            }

            return basketModel;
        }


        /// <summary>
        /// This method calculates the sub-total for each item in basket
        /// </summary>
        /// <param name="unitPrice">Unit price of item</param>
        /// <param name="quantity">Quantity of item</param>
        /// <param name="discount">Discount rate</param>
        /// <returns>float</returns>
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
