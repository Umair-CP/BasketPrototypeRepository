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

        /// <summary>
        /// This method produces an order for the customer. This method would generally be called after the customer 
        /// chooses to checkout.
        /// </summary>
        /// <param name="basketModel">Basket Model</param>
        /// <param name="customerModel">Customer Model</param>
        /// <param name="tax">Tax rate</param>
        /// <param name="deliveryPrice">Delivery price</param>
        /// <returns>Order Model</returns>
        public OrderModel ProduceOrder (BasketModel basketModel, CustomerModel customerModel, float tax, float deliveryPrice)
        {
            OrderModel orderModel = null;

            if(!String.IsNullOrEmpty(customerModel.Id) && !String.IsNullOrEmpty(customerModel.Name) && !String.IsNullOrEmpty(customerModel.DeliveryAddress) 
                && (tax >= 0 && tax <= 100) && deliveryPrice >= 0 && basketModel.TotalBasketPrice >= 0)
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


        /// <summary>
        /// This method calculates the final price of the order based on basket total price, delivery price and tax rate.
        /// </summary>
        /// <param name="basketTotal">Total price of all items in basket</param>
        /// <param name="tax">Tax rate</param>
        /// <param name="deliveryPrice">Delivery price</param>
        /// <returns>float</returns>
        private float CalculateOrderPrice(float basketTotal, float tax, float deliveryPrice)
        {
            float taxPrice = (basketTotal + deliveryPrice) * tax / 100;
            return (float)Math.Round(basketTotal + deliveryPrice + taxPrice, 2);
        }
    }
}
