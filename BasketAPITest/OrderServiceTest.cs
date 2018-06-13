using System;
using Xunit;
using BasketAPI.Services;
using BasketAPI.Models;

namespace BasketAPITest
{
    public class OrderServiceTest
    {
        [Fact]
        public void ProduceOrder()
        {
            OrderService orderService = new OrderService();

            BasketService basketService = new BasketService();
            BasketModel basketModel = basketService.CreateNewBasket("C123");
            BasketItemModel basketItemModel = new BasketItemModel();
            basketItemModel.Id = "111";
            basketItemModel.Name = "Car";
            basketItemModel.Quantity = 1;
            basketItemModel.UnitPrice = (float)5.50;
            bool isAdded = basketService.AddBasketItem("C123", basketModel.Id, basketItemModel);

            CustomerModel customerModel = new CustomerModel();
            customerModel.Id = "ABC";
            customerModel.ContactNumber = "0777777777";
            customerModel.DeliveryAddress = "5th Street Somewhere";
            customerModel.Name = "John Doe";

            var obj = orderService.ProduceOrder(basketModel, customerModel, (float)120.00, 0);

            Assert.Null(obj);

        }
    }
}
