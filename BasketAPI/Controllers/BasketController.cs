using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Interfaces;
using BasketAPI.Models;
using BasketAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Basket")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        /// <summary>
        /// This method creates a new basket and returns back a unique basket ID 
        /// </summary>
        /// <param name="id">This is the client ID</param>
        /// <returns>Either null if basket is not created or basket ID</returns>
        [Route("CreateNewBasket")]
        [HttpGet]
        public IActionResult CreateNewBasket(string id)
        {             
            BasketModel basketModel = _basketService.CreateNewBasket(id);

            if (basketModel != null)
                return new JsonResult(basketModel.Id);
            else return new JsonResult("Invalid client!");
        }


        /// <summary>
        /// This method adds an item to a basket. 
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemId">Item ID</param>
        /// <param name="name">Name of item</param>
        /// <param name="description">Description of item</param>
        /// <param name="specification">Specification of item</param>
        /// <param name="comment">User comments</param>
        /// <param name="unitPrice">Unit price</param>
        /// <param name="quantity">Qauntity</param>
        /// <param name="discount">Discount rate</param>
        /// <returns>True if add item was successful otherwise false</returns>
        [Route("AddBasketItem")]
        [HttpGet]
        public IActionResult AddBasketItem(string clientId, string basketId, string itemId, string name, string description, string specification,
                                            string comment, float unitPrice, int quantity, float discount)
        {
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = itemId;
            basketItemModel.Name = name;
            basketItemModel.Description = description;
            basketItemModel.Specification = specification;
            basketItemModel.Comment = comment;
            basketItemModel.UnitPrice = unitPrice;
            basketItemModel.Quantity = quantity;
            basketItemModel.Discount = discount;

            bool isAdded = _basketService.AddBasketItem(clientId, basketId, basketItemModel);
            return new JsonResult(isAdded);
        }


        /// <summary>
        /// This method removes a basket item
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <param name="itemId">Item ID of item to be removed from basket</param>
        /// <returns>True if remove item was successful otherwise false</returns>
        [Route("RemoveBasketItem")]
        [HttpGet]
        public IActionResult RemoveBasketItem(string clientId, string basketId, string itemId)
        {
            bool isRemoved = _basketService.RemoveBasketItem(clientId, basketId, itemId);
            return new JsonResult(isRemoved);
        }


        /// <summary>
        /// This method updates the quantity of an item in the basket
        /// </summary>
        /// <param name="clientId">Client Id</param>
        /// <param name="basketId">Basket Id</param>
        /// <param name="itemId">Item ID to be updated</param>
        /// <param name="quantity">New quantity</param>
        /// <returns>True if quantity update was successful otherwise false</returns>
        [Route("UpdateItemQuantity")]
        [HttpGet]
        public IActionResult UpdateItemQuantity(string clientId, string basketId, string itemId, int quantity)
        {
            bool isUpdated = _basketService.UpdateBasketItemQuantity(clientId, basketId, itemId, quantity);
            return new JsonResult(isUpdated);
        }


        /// <summary>
        /// This method clears all items in basket
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <returns>True if clear basket was successful otherwise false</returns>
        [Route("ClearBasket")]
        [HttpGet]
        public IActionResult ClearBasket(string clientId, string basketId)
        {
            bool isCleared = _basketService.ClearBasket(clientId, basketId);
            return new JsonResult(isCleared);
        }


        /// <summary>
        /// This method deletes the basket
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <returns>True if remove basket was successful otherwise false</returns>
        [Route("RemoveBasket")]
        [HttpGet]
        public IActionResult RemoveBasket(string clientId, string basketId)
        {
            bool isRemoved = _basketService.RemoveBasket(clientId, basketId);
            return new JsonResult(isRemoved);
        }


        /// <summary>
        /// This method gets the basket and also displays total price of all items
        /// </summary>
        /// <param name="clientId">Client ID</param>
        /// <param name="basketId">Basket ID</param>
        /// <returns>Basket Model</returns>
        [Route("GetBasket")]
        [HttpGet]
        public IActionResult GetBasket(string clientId, string basketId)
        {
            BasketModel basketModel = _basketService.GetBasket(clientId, basketId);
            return new JsonResult(basketModel);
        }

    }
}