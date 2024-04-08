using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Mvc;
using Address = Core.Entities.OrderAggregate.Address;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order>CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAdderss);
        
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order>GetOrderByIdAsync(int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}