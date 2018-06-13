using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Models
{
    public class CustomerModel
    {
        //ID of customer assigned by client
        public string Id { get; set; }
        //Full name of customer
        public string Name { get; set; }
        //Delivery address of customer
        public string DeliveryAddress { get; set; }
        //Contact phone number of customer
        public string ContactNumber { get; set; }
    }
}
