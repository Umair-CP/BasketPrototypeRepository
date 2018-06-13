using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Models
{
    public class OrderModel
    {
        //ID of the order
        public string Id { get; set; }
        public CustomerModel Customer { get; set; }
        public BasketModel Basket { get; set; }
        //Delivery price
        public float DeliveryPrice { get; set; }
        //Tax rate in percentage
        public float Tax { get; set; }
        //Calculated number of items
        public int NumberOfItems { get; set; }
        //Calculated final price including basket, delivery and tax
        public float FinalOrderPrice { get; set; }
    }
}
