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

        [Route("CreateNewBasket")]
        [HttpGet]
        public IActionResult CreateNewBasket(string id)
        {             
            BasketModel basketModel = _basketService.CreateNewBasket(id);

            if (basketModel != null)
                return new JsonResult(basketModel.Id);
            else return new JsonResult("Invalid client!");
        }

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

            bool isAdded = _basketService.AddBasketItem(clientId, basketId, basketItemModel);
            return new JsonResult(isAdded);
        }


        [Route("RemoveBasketItem")]
        [HttpGet]
        public IActionResult RemoveBasketItem(string clientId, string basketId, string itemId)
        {
            bool isRemoved = _basketService.RemoveBasketItem(clientId, basketId, itemId);
            return new JsonResult(isRemoved);
        }

        [Route("UpdateItemQuantity")]
        [HttpGet]
        public IActionResult UpdateItemQuantity(string clientId, string basketId, string itemId, int quantity)
        {
            bool isUpdated = _basketService.UpdateBasketItemQuantity(clientId, basketId, itemId, quantity);
            return new JsonResult(isUpdated);
        }

        [Route("ClearBasket")]
        [HttpGet]
        public IActionResult ClearBasket(string clientId, string basketId)
        {
            bool isCleared = _basketService.ClearBasket(clientId, basketId);
            return new JsonResult(isCleared);
        }

        [Route("RemoveBasket")]
        [HttpGet]
        public IActionResult RemoveBasket(string clientId, string basketId)
        {
            bool isRemoved = _basketService.RemoveBasket(clientId, basketId);
            return new JsonResult(isRemoved);
        }

        [Route("GetBasket")]
        [HttpGet]
        public IActionResult GetBasket(string clientId, string basketId)
        {
            BasketModel basketModel = _basketService.GetBasket(clientId, basketId);
            return new JsonResult(basketModel);
        }

    }
}