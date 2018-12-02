using smileRed25.Domain;
using smileRed25.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace smileRed25.Helpers
{
    public class ConverterOrder
    {
        public static OrderView ToOrder(Order order)
        {
            return new OrderView
            {
               OrderID = order.OrderID,
               UserId = order.UserId,
               Email = order.Email,
               DateOrder = order.DateOrder,
               OrderStatusID = order.OrderStatusID,
               Delete = order.Delete,
               VisibleOrders = order.VisibleOrders,
               ActiveOrders = order.ActiveOrders,
            };
        }

        public static Order ToOrderDomain(Order order)
        {
            return new Order
            {
                OrderID = order.OrderID,
                UserId = order.UserId,
                Email = order.Email,
                DateOrder = order.DateOrder,
                OrderStatusID = order.OrderStatusID,
                Delete = order.Delete,
                VisibleOrders = order.VisibleOrders,
                ActiveOrders = order.ActiveOrders,
            };
        }

        public static OrderDetails ToOrderDetailsDomain(OrderDetails orderD)
        {
            return new OrderDetails
            {
                OrderDetailsID = orderD.OrderDetailsID,
                OrderID = orderD.OrderID,
                ActiveOrderDetails = false,
                VisibleOrderDetails = false,
                Ingredients = orderD.Ingredients,
                Quantity = orderD.Quantity,
                ProductID = orderD.ProductID,
            };
        }
    }
}
