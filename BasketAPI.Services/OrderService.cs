using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BasketAPI.Interfaces;
using BasketAPI.Models;

namespace BasketAPI.Services
{
    public class OrderService : IOrderService
    {

        public OrderModel ProduceOrder (BasketModel basketModel, CustomerModel customerModel, float tax, float deliveryPrice)
        {
            OrderModel orderModel = null;

            if(!String.IsNullOrEmpty(customerModel.Id) && !String.IsNullOrEmpty(customerModel.Name) && !String.IsNullOrEmpty(customerModel.DeliveryAddress) 
                && (tax >= 0 && tax <= 100) && deliveryPrice >= 0)
            {
                orderModel = new OrderModel();
                orderModel.Basket = basketModel;
                orderModel.Customer = customerModel;
                orderModel.Tax = tax;
                orderModel.DeliveryPrice = deliveryPrice;
                orderModel.NumberOfItems = basketModel.BasketItems.Count;
                orderModel.FinalOrderPrice = CalculateOrderPrice(basketModel.TotalBasketPrice, tax, deliveryPrice);

            }

            return orderModel;

        }

        private float CalculateOrderPrice(float basketTotal, float tax, float deliveryPrice)
        {
            float taxPrice = (basketTotal + deliveryPrice) * tax / 100;
            return (float)Math.Round(basketTotal + deliveryPrice + tax, 2);
        }
    }
}
