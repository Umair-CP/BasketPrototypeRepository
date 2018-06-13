using BasketAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Interfaces
{
    public interface IOrderService
    {
        OrderModel ProduceOrder(BasketModel basketModel, CustomerModel customerModel, float tax, float deliveryPrice);
    }
}
