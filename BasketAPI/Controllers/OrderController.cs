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
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBasketService _basketService;
        public OrderController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        [Route("ProduceOrder")]
        [HttpGet]
        public IActionResult ProduceOrder(string clientId, string basketId, string customerId, string customerName, string deliveryAddress, 
                                            string contactNumber, float tax, float deliveryPrice)
        {
            BasketModel basketModel = new BasketModel();
            basketModel = _basketService.GetBasket(clientId, basketId);
            CustomerModel customerModel = new CustomerModel();
            customerModel.Id = customerId;
            customerModel.Name = customerName;
            customerModel.DeliveryAddress = deliveryAddress;
            customerModel.ContactNumber = contactNumber;
      
            OrderModel orderModel = _orderService.ProduceOrder(basketModel, customerModel, tax, deliveryPrice);

            return new JsonResult(orderModel);
        }

    }
}