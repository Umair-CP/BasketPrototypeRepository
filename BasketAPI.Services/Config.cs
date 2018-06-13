using BasketAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BasketAPI.Services
{
    public class Config
    {
        //It is assumed that clients will have registered beforehand to use the prototye. 
        //The clients will be aware of their Client IDs which would be necessary to query the the APIs.
        //In real life however, a client would be issued with a public and private key so that they could 
        //be authenticated first and then authorised to use the appropriate APIs.
        public static List<ClientModel> GetClients()
        {
            return new List<ClientModel>
            {
                new ClientModel
                {
                    Id = "C123",
                    Name = "Client 1"
                },
                new ClientModel
                {
                    Id = "C456",
                    Name = "Client 2"
                },
                new ClientModel
                {
                    Id = "C789",
                    Name = "Client 3"
                }
            };
        }

        public static List<BasketModel> BasketData = new List<BasketModel>();
        public static void SaveBasketData(BasketModel basket)
        {
            BasketData.Add(basket);
        }
    }
}
