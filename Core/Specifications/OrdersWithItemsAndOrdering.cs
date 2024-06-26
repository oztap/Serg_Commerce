using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrdering : BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrdering(string email): base(o=>o.BuyerEmail==email)
        {
            Addinclude(o=>o.OrderItems);
            Addinclude(o=>o.DeliveryMethod);
            AddOrderByDescending(o=>o.OrderDate);
        }

        public OrdersWithItemsAndOrdering(int id,string email) : base(o=>o.Id==id && o.BuyerEmail==email)
        {
            Addinclude(o=>o.OrderItems);
            Addinclude(o=>o.DeliveryMethod);
        }
    }
}